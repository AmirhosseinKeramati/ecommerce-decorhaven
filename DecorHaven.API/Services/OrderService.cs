using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DecorHaven.API.Data;
using DecorHaven.API.DTOs.Orders;
using DecorHaven.API.Models;
using DecorHaven.API.Repositories;

namespace DecorHaven.API.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId)
    {
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int orderId, int userId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

        if (order == null)
            return null;

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto?> CreateOrderAsync(int userId, CreateOrderDto createOrderDto)
    {
        // Get cart items
        var cartItems = await _context.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.UserId == userId)
            .ToListAsync();

        if (!cartItems.Any())
            return null;

        // Calculate totals
        var subTotal = cartItems.Sum(ci => ci.Product.Price * ci.Quantity);
        var shippingCost = subTotal >= 200 ? 0 : 20; // Free shipping over $200
        var tax = subTotal * 0.1m; // 10% tax
        var totalAmount = subTotal + shippingCost + tax;

        // Create order
        var order = new Order
        {
            UserId = userId,
            OrderNumber = GenerateOrderNumber(),
            SubTotal = subTotal,
            ShippingCost = shippingCost,
            Tax = tax,
            TotalAmount = totalAmount,
            PaymentMethod = createOrderDto.PaymentMethod,
            ShippingAddress = createOrderDto.ShippingAddress,
            ShippingCity = createOrderDto.ShippingCity,
            ShippingPostalCode = createOrderDto.ShippingPostalCode,
            ShippingCountry = createOrderDto.ShippingCountry,
            CustomerNotes = createOrderDto.CustomerNotes,
            Status = "Pending",
            PaymentStatus = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        var orderRepository = _unitOfWork.Repository<Order>();
        await orderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        // Create order items
        var orderItems = cartItems.Select(ci => new OrderItem
        {
            OrderId = order.Id,
            ProductId = ci.ProductId,
            ProductName = ci.Product.Name,
            Price = ci.Product.Price,
            Quantity = ci.Quantity,
            SubTotal = ci.Product.Price * ci.Quantity
        }).ToList();

        var orderItemRepository = _unitOfWork.Repository<OrderItem>();
        await orderItemRepository.AddRangeAsync(orderItems);

        // Clear cart
        var cartItemRepository = _unitOfWork.Repository<CartItem>();
        await cartItemRepository.DeleteRangeAsync(cartItems);

        // Update product stock
        foreach (var cartItem in cartItems)
        {
            cartItem.Product.StockQuantity -= cartItem.Quantity;
        }

        await _unitOfWork.SaveChangesAsync();

        // Return created order
        var createdOrder = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstAsync(o => o.Id == order.Id);

        return _mapper.Map<OrderDto>(createdOrder);
    }

    public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            return false;

        order.Status = status;
        order.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
    }
}

