using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DecorHaven.API.DTOs.Common;
using DecorHaven.API.DTOs.Orders;
using DecorHaven.API.Services;
using System.Security.Claims;

namespace DecorHaven.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<OrderDto>>>> GetUserOrders()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var orders = await _orderService.GetUserOrdersAsync(userId);
        return Ok(ApiResponse<IEnumerable<OrderDto>>.SuccessResponse(orders));
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<ApiResponse<OrderDto>>> GetOrder(int orderId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var order = await _orderService.GetOrderByIdAsync(orderId, userId);

        if (order == null)
        {
            return NotFound(ApiResponse<OrderDto>.ErrorResponse("Order not found"));
        }

        return Ok(ApiResponse<OrderDto>.SuccessResponse(order));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<OrderDto>>> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var order = await _orderService.CreateOrderAsync(userId, createOrderDto);

        if (order == null)
        {
            return BadRequest(ApiResponse<OrderDto>.ErrorResponse("Cannot create order. Cart is empty."));
        }

        return CreatedAtAction(nameof(GetOrder), new { orderId = order.Id }, 
            ApiResponse<OrderDto>.SuccessResponse(order, "Order created successfully"));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{orderId}/status")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateOrderStatus(int orderId, [FromBody] string status)
    {
        var result = await _orderService.UpdateOrderStatusAsync(orderId, status);

        if (!result)
        {
            return NotFound(ApiResponse<bool>.ErrorResponse("Order not found"));
        }

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Order status updated"));
    }
}

