using OrderManagement.Application.DTO.Order;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interface;

public interface IOrderRepo
{
    Task<List<DisplayOrder>> GetOrdersAsync();
    Task<DisplayOrder> GetOrderByIdAsync(Guid id);

    Task AddOrder(Order order);

    Task RemoveOrder(Order order);
}