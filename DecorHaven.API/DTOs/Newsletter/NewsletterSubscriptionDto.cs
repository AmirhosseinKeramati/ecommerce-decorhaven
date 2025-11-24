using System.ComponentModel.DataAnnotations;

namespace DecorHaven.API.DTOs.Newsletter;

public class NewsletterSubscriptionDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}

