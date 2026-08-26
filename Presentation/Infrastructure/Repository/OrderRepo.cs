using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repo;

public class OrderRepo : IOrderRepo
{
    private readonly AppDbContext _db;

    public OrderRepo(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return await _db.Orders.AsNoTracking().ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        return await _db.Orders.FirstOrDefaultAsync(order => order.Id == orderId);
    }

    public async Task AddOrder(Order order)
    {
        _db.Orders.Add(order);
    }

    public async Task RemoveOrder(Order order)
    {
        _db.Orders.Remove(order);
    }
}