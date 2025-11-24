using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DecorHaven.API.Data;
using DecorHaven.API.DTOs.Products;
using DecorHaven.API.Models;
using DecorHaven.API.Repositories;

namespace DecorHaven.API.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId && p.IsActive)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return null;

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        var productRepository = _unitOfWork.Repository<Product>();

        var product = _mapper.Map<Product>(createProductDto);
        product.CreatedAt = DateTime.UtcNow;

        await productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductDto>(await _context.Products
            .Include(p => p.Category)
            .FirstAsync(p => p.Id == product.Id));
    }

    public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return null;

        if (updateProductDto.Name != null)
            product.Name = updateProductDto.Name;

        if (updateProductDto.Description != null)
            product.Description = updateProductDto.Description;

        if (updateProductDto.Price.HasValue)
            product.Price = updateProductDto.Price.Value;

        if (updateProductDto.OldPrice.HasValue)
            product.OldPrice = updateProductDto.OldPrice;

        if (updateProductDto.CategoryId.HasValue)
            product.CategoryId = updateProductDto.CategoryId.Value;

        if (updateProductDto.ImageUrl != null)
            product.ImageUrl = updateProductDto.ImageUrl;

        if (updateProductDto.IconClass != null)
            product.IconClass = updateProductDto.IconClass;

        if (updateProductDto.StockQuantity.HasValue)
            product.StockQuantity = updateProductDto.StockQuantity.Value;

        if (updateProductDto.IsNew.HasValue)
            product.IsNew = updateProductDto.IsNew.Value;

        if (updateProductDto.IsFeatured.HasValue)
            product.IsFeatured = updateProductDto.IsFeatured.Value;

        if (updateProductDto.IsActive.HasValue)
            product.IsActive = updateProductDto.IsActive.Value;

        product.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var productRepository = _unitOfWork.Repository<Product>();
        var product = await productRepository.GetByIdAsync(id);

        if (product == null)
            return false;

        product.IsActive = false;
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive && 
                   (p.Name.Contains(searchTerm) || 
                    p.Description.Contains(searchTerm) || 
                    p.Category.Name.Contains(searchTerm)))
            .ToListAsync();

        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}

