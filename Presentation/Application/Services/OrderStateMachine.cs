using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enum;

namespace OrderManagement.Application.Services;

public class OrderStateMachine
{
    public bool IsValidTransition(OrderStatus currentStatus, OrderStatus newStatus)
    {
        return (currentStatus, newStatus) switch
        {
            // Order Flow
            (OrderStatus.Received, OrderStatus.Preparing) => true,
            (OrderStatus.Preparing, OrderStatus.Served) => true,
            (OrderStatus.Ready, OrderStatus.Served) => true,
            (OrderStatus.Served, OrderStatus.Completed) => true,

            //Cancellation Flow
            (OrderStatus.Received, OrderStatus.Cancelled) => true,
            (OrderStatus.Preparing, OrderStatus.Cancelled) => true,
            (OrderStatus.Ready, OrderStatus.Cancelled) => true,

            _ => false
        };
    }
}