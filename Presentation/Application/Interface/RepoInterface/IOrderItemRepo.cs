using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interface;

public interface IOrderItemRepo
{
    Task<List<OrderItem>> GetOrderItemsAsync();
    Task<OrderItem> GetOrderItemsById(Guid id);

    Task AddOrderItem(OrderItem orderItem);

    Task RemoveOrderItem(OrderItem orderItem);
}