using DecorHaven.API.DTOs.Newsletter;
using DecorHaven.API.Models;
using DecorHaven.API.Repositories;

namespace DecorHaven.API.Services;

public class NewsletterService : INewsletterService
{
    private readonly IUnitOfWork _unitOfWork;

    public NewsletterService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> SubscribeAsync(NewsletterSubscriptionDto subscriptionDto)
    {
        var newsletterRepository = _unitOfWork.Repository<Newsletter>();

        // Check if already subscribed
        var existing = await newsletterRepository.FirstOrDefaultAsync(n => n.Email == subscriptionDto.Email);
        if (existing != null)
        {
            if (!existing.IsActive)
            {
                existing.IsActive = true;
                await _unitOfWork.SaveChangesAsync();
            }
            return true;
        }

        // Create new subscription
        var newsletter = new Newsletter
        {
            Email = subscriptionDto.Email,
            IsActive = true,
            SubscribedAt = DateTime.UtcNow
        };

        await newsletterRepository.AddAsync(newsletter);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UnsubscribeAsync(string email)
    {
        var newsletterRepository = _unitOfWork.Repository<Newsletter>();
        var subscription = await newsletterRepository.FirstOrDefaultAsync(n => n.Email == email);

        if (subscription == null)
            return false;

        subscription.IsActive = false;
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}

