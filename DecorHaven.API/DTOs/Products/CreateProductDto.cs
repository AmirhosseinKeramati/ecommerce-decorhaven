using System.ComponentModel.DataAnnotations;

namespace DecorHaven.API.DTOs.Products;

public class CreateProductDto
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    public decimal? OldPrice { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public string? ImageUrl { get; set; }

    public string? IconClass { get; set; }

    public int StockQuantity { get; set; } = 0;

    public bool IsNew { get; set; } = false;

    public bool IsFeatured { get; set; } = false;
}

