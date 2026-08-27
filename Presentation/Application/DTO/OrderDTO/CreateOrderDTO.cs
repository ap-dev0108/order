using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enum;

namespace OrderManagement.Application.DTO.Orders;

public class CreateOrder
{
    public Guid DinningSessionId { get; set; }
    public DinningSession DinningSessions { get; set; }

    public OrderStatus OrderStatus { get; set; }
}

public class CreateOrderItem
{
    public Guid OrderId { get; set; }
    public Order Orders { get; set; }

    public Guid MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPriceAtOrder { get; set; }

    public string Notes { get; set; } = string.Empty;
}