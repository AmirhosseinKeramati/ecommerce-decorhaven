using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DecorHaven.API.Data;
using DecorHaven.API.DTOs.Cart;
using DecorHaven.API.Models;
using DecorHaven.API.Repositories;

namespace DecorHaven.API.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public CartService(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync(int userId)
    {
        var cartItems = await _context.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.UserId == userId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CartItemDto>>(cartItems);
    }

    public async Task<CartItemDto?> AddToCartAsync(int userId, AddToCartDto addToCartDto)
    {
        var cartItemRepository = _unitOfWork.Repository<CartItem>();
        var productRepository = _unitOfWork.Repository<Product>();

        // Check if product exists and has stock
        var product = await productRepository.GetByIdAsync(addToCartDto.ProductId);
        if (product == null || product.StockQuantity < addToCartDto.Quantity)
            return null;

        // Check if item already exists in cart
        var existingCartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == addToCartDto.ProductId);

        if (existingCartItem != null)
        {
            existingCartItem.Quantity += addToCartDto.Quantity;
            await _unitOfWork.SaveChangesAsync();

            var updatedItem = await _context.CartItems
                .Include(ci => ci.Product)
                .FirstAsync(ci => ci.Id == existingCartItem.Id);

            return _mapper.Map<CartItemDto>(updatedItem);
        }

        // Create new cart item
        var cartItem = new CartItem
        {
            UserId = userId,
            ProductId = addToCartDto.ProductId,
            Quantity = addToCartDto.Quantity,
            CreatedAt = DateTime.UtcNow
        };

        await cartItemRepository.AddAsync(cartItem);
        await _unitOfWork.SaveChangesAsync();

        var newItem = await _context.CartItems
            .Include(ci => ci.Product)
            .FirstAsync(ci => ci.Id == cartItem.Id);

        return _mapper.Map<CartItemDto>(newItem);
    }

    public async Task<bool> UpdateCartItemAsync(int userId, int cartItemId, int quantity)
    {
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.UserId == userId);

        if (cartItem == null)
            return false;

        cartItem.Quantity = quantity;
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveFromCartAsync(int userId, int cartItemId)
    {
        var cartItemRepository = _unitOfWork.Repository<CartItem>();
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.UserId == userId);

        if (cartItem == null)
            return false;

        await cartItemRepository.DeleteAsync(cartItem);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ClearCartAsync(int userId)
    {
        var cartItems = await _context.CartItems
            .Where(ci => ci.UserId == userId)
            .ToListAsync();

        if (!cartItems.Any())
            return false;

        var cartItemRepository = _unitOfWork.Repository<CartItem>();
        await cartItemRepository.DeleteRangeAsync(cartItems);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<decimal> GetCartTotalAsync(int userId)
    {
        var total = await _context.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.UserId == userId)
            .SumAsync(ci => ci.Product.Price * ci.Quantity);

        return total;
    }
}

