using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DecorHaven.API.DTOs.Cart;
using DecorHaven.API.DTOs.Common;
using DecorHaven.API.Services;
using System.Security.Claims;

namespace DecorHaven.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CartItemDto>>>> GetCartItems()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var cartItems = await _cartService.GetCartItemsAsync(userId);
        return Ok(ApiResponse<IEnumerable<CartItemDto>>.SuccessResponse(cartItems));
    }

    [HttpGet("total")]
    public async Task<ActionResult<ApiResponse<decimal>>> GetCartTotal()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var total = await _cartService.GetCartTotalAsync(userId);
        return Ok(ApiResponse<decimal>.SuccessResponse(total));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CartItemDto>>> AddToCart([FromBody] AddToCartDto addToCartDto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var cartItem = await _cartService.AddToCartAsync(userId, addToCartDto);

        if (cartItem == null)
        {
            return BadRequest(ApiResponse<CartItemDto>.ErrorResponse("Product not available or insufficient stock"));
        }

        return Ok(ApiResponse<CartItemDto>.SuccessResponse(cartItem, "Item added to cart"));
    }

    [HttpPut("{cartItemId}")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateCartItem(int cartItemId, [FromBody] int quantity)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var result = await _cartService.UpdateCartItemAsync(userId, cartItemId, quantity);

        if (!result)
        {
            return NotFound(ApiResponse<bool>.ErrorResponse("Cart item not found"));
        }

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Cart item updated"));
    }

    [HttpDelete("{cartItemId}")]
    public async Task<ActionResult<ApiResponse<bool>>> RemoveFromCart(int cartItemId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var result = await _cartService.RemoveFromCartAsync(userId, cartItemId);

        if (!result)
        {
            return NotFound(ApiResponse<bool>.ErrorResponse("Cart item not found"));
        }

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Item removed from cart"));
    }

    [HttpDelete]
    public async Task<ActionResult<ApiResponse<bool>>> ClearCart()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var result = await _cartService.ClearCartAsync(userId);

        if (!result)
        {
            return NotFound(ApiResponse<bool>.ErrorResponse("Cart is already empty"));
        }

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Cart cleared"));
    }
}

