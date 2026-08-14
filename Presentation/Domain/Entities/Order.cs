using OrderManagement.Domain.Enum;

namespace OrderManagement.Domain.Entities;

public class Order
{
    public Guid Id {get; set;} = Guid.NewGuid();
    
    public Guid DinningSessionId {get; set;}
    public DinningSession DinningSessions {get; set;}

    public OrderStatus OrderStatus {get; set;}
}