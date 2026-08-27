using OrderManagement.Application.DTO.Orders;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Services;

public class OrderItemServices
{
    private readonly IOrderItemRepo _orderItem;
    private readonly IDataRepo _data;
    public OrderItemServices(IOrderItemRepo orderItem, IDataRepo data)
    {
        _orderItem = orderItem;
        _data = data;
    }

    public async Task<List<DisplayOrderItem>> GetOrderItemsAsync()
    {
        var orderItemList = await _orderItem.GetOrderItemsAsync() ??
            throw new KeyNotFoundException("Order Item cannot be found");

        return orderItemList.Select(item => new DisplayOrderItem
        {
            Id = item.Id,
            OrderId = item.OrderId,
            Orders = item.Orders,
            UnitPriceAtOrder = item.UnitPriceAtOrder,
            MenuItemId = item.MenuItemId,
            MenuItem = item.MenuItem,
            Quantity = item.Quantity
        }).ToList();
    }

    public async Task<DisplayOrderItem> GetOrderItemsById(Guid itemsId)
    {
        var item = await _orderItem.GetOrderItemsById(itemsId) ??
            throw new KeyNotFoundException($"Item with the ID: {itemsId} cannot be found");

        return new DisplayOrderItem
        {
            Id = item.Id,
            OrderId = item.OrderId,
            Orders = item.Orders,
            UnitPriceAtOrder = item.UnitPriceAtOrder,
            MenuItemId = item.MenuItemId,
            MenuItem = item.MenuItem,
            Quantity = item.Quantity
        };
    }

    public async Task<DisplayOrderItem> GetOrderItemByOrder(Guid orderId)
    {
        var order = await _orderItem.GetOrderItemByOrder(orderId) ??
            throw new KeyNotFoundException($"Order with the ID: {orderId} cannot be found");

        return new DisplayOrderItem
        {
            Id = order.Id,
            OrderId = orderId,
            Orders = order.Orders,
            UnitPriceAtOrder = order.UnitPriceAtOrder,
            MenuItem = order.MenuItem,
            MenuItemId = order.MenuItemId,
            Quantity = order.Quantity
        };
    }

    public async Task AddOrderItem(Guid orderId, CreateOrderItem createOrderItem)
    {
        var checkOrderExists = await _orderItem.GetOrderItemByOrder(orderId);
        if (checkOrderExists == null)
        {
            throw new KeyNotFoundException($"Order with the provided ID does not exists: {orderId}");
        }

        if (checkOrderExists.Orders == null)
        {
            throw new InvalidOperationException("Cannot add item since order was not created");
        }

        var itemToAdd = new OrderItem
        {
            OrderId = orderId,
            Orders = checkOrderExists.Orders,
            MenuItem = createOrderItem.MenuItem,
            MenuItemId = createOrderItem.MenuItemId,
            Quantity = createOrderItem.Quantity,
            UnitPriceAtOrder = createOrderItem.UnitPriceAtOrder
        };

        await _orderItem.AddOrderItem(itemToAdd);
        await _data.SaveChangesAsync();
    }

    public async Task RemoveOrderItem(Guid orderId)
    {
        var order = await _orderItem.GetOrderItemsById(orderId) ??
            throw new KeyNotFoundException($"Order with the given ID cannot be found: {orderId}");

        await _orderItem.RemoveOrderItem(order);
        await _data.SaveChangesAsync();
    }
}