using System.ComponentModel.DataAnnotations;

namespace DecorHaven.API.Models;

public class Newsletter
{
    [Key]
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
}

