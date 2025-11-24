using System.ComponentModel.DataAnnotations;

namespace DecorHaven.API.DTOs.Orders;

public class CreateOrderDto
{
    [Required]
    public string PaymentMethod { get; set; } = string.Empty;

    [Required]
    public string ShippingAddress { get; set; } = string.Empty;

    [Required]
    public string ShippingCity { get; set; } = string.Empty;

    [Required]
    public string ShippingPostalCode { get; set; } = string.Empty;

    [Required]
    public string ShippingCountry { get; set; } = string.Empty;

    public string? CustomerNotes { get; set; }
}

