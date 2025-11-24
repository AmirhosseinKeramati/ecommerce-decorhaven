using DecorHaven.API.DTOs.Newsletter;

namespace DecorHaven.API.Services;

public interface INewsletterService
{
    Task<bool> SubscribeAsync(NewsletterSubscriptionDto subscriptionDto);
    Task<bool> UnsubscribeAsync(string email);
}

