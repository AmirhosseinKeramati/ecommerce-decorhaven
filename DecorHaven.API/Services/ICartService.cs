using DecorHaven.API.DTOs.Cart;

namespace DecorHaven.API.Services;

public interface ICartService
{
    Task<IEnumerable<CartItemDto>> GetCartItemsAsync(int userId);
    Task<CartItemDto?> AddToCartAsync(int userId, AddToCartDto addToCartDto);
    Task<bool> UpdateCartItemAsync(int userId, int cartItemId, int quantity);
    Task<bool> RemoveFromCartAsync(int userId, int cartItemId);
    Task<bool> ClearCartAsync(int userId);
    Task<decimal> GetCartTotalAsync(int userId);
}

