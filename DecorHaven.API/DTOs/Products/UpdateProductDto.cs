using System.ComponentModel.DataAnnotations;

namespace DecorHaven.API.DTOs.Products;

public class UpdateProductDto
{
    [StringLength(200)]
    public string? Name { get; set; }

    public string? Description { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal? Price { get; set; }

    public decimal? OldPrice { get; set; }

    public int? CategoryId { get; set; }

    public string? ImageUrl { get; set; }

    public string? IconClass { get; set; }

    public int? StockQuantity { get; set; }

    public bool? IsNew { get; set; }

    public bool? IsFeatured { get; set; }

    public bool? IsActive { get; set; }
}

