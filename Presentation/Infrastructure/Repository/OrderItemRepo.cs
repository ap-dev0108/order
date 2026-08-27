using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repo;

public class OrderItemRepo : IOrderItemRepo
{
    private readonly AppDbContext _db;

    public OrderItemRepo(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<OrderItem>> GetOrderItemsAsync()
    {
        return await _db.OrderItems.AsNoTracking().ToListAsync();
    }

    public async Task<OrderItem> GetOrderItemsById(Guid itemId)
    {
        return await _db.OrderItems.FirstOrDefaultAsync(item => item.Id == itemId);
    }

    public async Task<OrderItem> GetOrderItemByOrder(Guid orderId)
    {
        return await _db.OrderItems.FirstOrDefaultAsync(order => order.OrderId == orderId);
    }
        
    public async Task AddOrderItem(OrderItem orderItem)
    {
        _db.OrderItems.Add(orderItem);
    }

    public async Task RemoveOrderItem(OrderItem orderItem)
    {
        _db.OrderItems.Remove(orderItem);
    }
}