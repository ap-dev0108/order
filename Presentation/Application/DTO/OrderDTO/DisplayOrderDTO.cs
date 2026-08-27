using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enum;

namespace OrderManagement.Application.DTO.Orders;

public class DisplayOrder
{
    public Guid Id {get; set;} = Guid.NewGuid();
    
    public Guid DinningSessionId {get; set;}
    public DinningSession DinningSessions {get; set;}

    public OrderStatus OrderStatus {get; set;}
}

public class DisplayOrderItem
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public Guid OrderId {get; set;}
    public Order Orders {get; set;}

    public Guid MenuItemId {get; set;}
    public MenuItem MenuItem {get; set;}

    public int Quantity {get; set;}
    public decimal UnitPriceAtOrder {get; set;}

    public string Notes {get; set;} = string.Empty;
}