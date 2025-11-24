using System.ComponentModel.DataAnnotations;

namespace DecorHaven.API.DTOs.Auth;

public class RegisterDto
{
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    // Phone validation removed to allow null or empty values
    public string? PhoneNumber { get; set; }
}

