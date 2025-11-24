using DecorHaven.API.DTOs.Orders;

namespace DecorHaven.API.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId);
    Task<OrderDto?> GetOrderByIdAsync(int orderId, int userId);
    Task<OrderDto?> CreateOrderAsync(int userId, CreateOrderDto createOrderDto);
    Task<bool> UpdateOrderStatusAsync(int orderId, string status);
}

