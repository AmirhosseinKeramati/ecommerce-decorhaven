using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DecorHaven.API.DTOs.Common;
using DecorHaven.API.DTOs.Products;
using DecorHaven.API.Services;

namespace DecorHaven.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(ApiResponse<IEnumerable<ProductDto>>.SuccessResponse(products));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound(ApiResponse<ProductDto>.ErrorResponse("Product not found"));
        }

        return Ok(ApiResponse<ProductDto>.SuccessResponse(product));
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> GetProductsByCategory(int categoryId)
    {
        var products = await _productService.GetProductsByCategoryAsync(categoryId);
        return Ok(ApiResponse<IEnumerable<ProductDto>>.SuccessResponse(products));
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> SearchProducts([FromQuery] string q)
    {
        var products = await _productService.SearchProductsAsync(q);
        return Ok(ApiResponse<IEnumerable<ProductDto>>.SuccessResponse(products));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductDto>>> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        var product = await _productService.CreateProductAsync(createProductDto);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, 
            ApiResponse<ProductDto>.SuccessResponse(product, "Product created successfully"));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
    {
        var product = await _productService.UpdateProductAsync(id, updateProductDto);

        if (product == null)
        {
            return NotFound(ApiResponse<ProductDto>.ErrorResponse("Product not found"));
        }

        return Ok(ApiResponse<ProductDto>.SuccessResponse(product, "Product updated successfully"));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteProduct(int id)
    {
        var result = await _productService.DeleteProductAsync(id);

        if (!result)
        {
            return NotFound(ApiResponse<bool>.ErrorResponse("Product not found"));
        }

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Product deleted successfully"));
    }
}

