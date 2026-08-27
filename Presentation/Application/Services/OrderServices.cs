using OrderManagement.Application.DTO.Orders;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enum;

namespace OrderManagement.Application.Services;

public class OrderServices
{
    private readonly IOrderRepo _order;
    private readonly IDataRepo _data;
    private readonly IDinningRepo _dinning;
    private readonly OrderStateMachine _status;

    public OrderServices(IOrderRepo order, IDataRepo data, IDinningRepo dinning, OrderStateMachine status)
    {
        _order = order;
        _data = data;
        _dinning = dinning;
        _status = status;
    }

    public async Task<List<DisplayOrder>> GetOrdersAsync()
    {
        var orderList = await _order.GetOrdersAsync() ??
            throw new KeyNotFoundException("Order not found");

        return orderList.Select(order => new DisplayOrder
        {
            Id = order.Id,
            DinningSessionId = order.DinningSessionId,
            DinningSessions = order.DinningSessions,
            OrderStatus = order.OrderStatus
        }).ToList();
    }

    public async Task<DisplayOrder> GetOrderById(Guid orderId)
    {
        var order = await _order.GetOrderByIdAsync(orderId) ??
            throw new KeyNotFoundException($"Order with the given ID: {orderId} cannot be found");

        return new DisplayOrder
        {
            Id = order.Id,
            DinningSessionId = order.DinningSessionId,
            DinningSessions = order.DinningSessions,
            OrderStatus = order.OrderStatus
        };
    }

    public async Task AddOrders(CreateOrder createOrder)
    {
        var dinningExists = await _dinning.GetDinningSessionById(createOrder.DinningSessionId);
        if (dinningExists == null)
        {
            throw new KeyNotFoundException($"Dinning with the ID: {createOrder.DinningSessionId}" +
                                                "cannot be found. Order cannot be processed further");
        }

        var orderToAdd = new Order
        {
            DinningSessionId = createOrder.DinningSessionId,
            DinningSessions = createOrder.DinningSessions,
            OrderStatus = OrderStatus.Received
        };

        await _order.AddOrder(orderToAdd);
        await _data.SaveChangesAsync();
    }

    public async Task<ChangeOrderStatus> UpdateOrderStatus(Guid orderId, ChangeOrderStatus orderStatus)
    {
        var order = await _order.GetOrderByIdAsync(orderId) ??
            throw new KeyNotFoundException($"Order with ID: {orderId} cannot be found");

        order.OrderStatus = orderStatus.orderStatus;

        if (_status.IsValidTransition(order.OrderStatus, orderStatus.orderStatus))
        {
            throw new InvalidOperationException("Invalid transaction" +
                                                    $"{order.OrderStatus} -> {orderStatus.orderStatus}");
        }

        await _data.SaveChangesAsync();

        return new ChangeOrderStatus
        {
            orderStatus = orderStatus.orderStatus
        };
    }

    public async Task RemoveOrder(Guid orderId)
    {
        var order = await _order.GetOrderByIdAsync(orderId) ??
            throw new KeyNotFoundException($"Order with ID: {orderId} cannot be found");

        await _order.RemoveOrder(order);
        await _data.SaveChangesAsync();
    }
}