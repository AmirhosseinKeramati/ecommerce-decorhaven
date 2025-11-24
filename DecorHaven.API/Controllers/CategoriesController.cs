using Microsoft.AspNetCore.Mvc;
using DecorHaven.API.DTOs.Categories;
using DecorHaven.API.DTOs.Common;
using DecorHaven.API.Services;

namespace DecorHaven.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDto>>>> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(ApiResponse<IEnumerable<CategoryDto>>.SuccessResponse(categories));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> GetCategory(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return NotFound(ApiResponse<CategoryDto>.ErrorResponse("Category not found"));
        }

        return Ok(ApiResponse<CategoryDto>.SuccessResponse(category));
    }
}

