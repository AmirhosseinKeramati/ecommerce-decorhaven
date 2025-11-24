using DecorHaven.API.DTOs.Categories;

namespace DecorHaven.API.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
}

