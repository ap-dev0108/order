using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enum;

namespace OrderManagement.Application.DTO.Order;

public class CreateOrder
{
    public Guid DinningSessionId {get; set;}
    public DinningSession DinningSessions {get; set;}

    public OrderStatus OrderStatus {get; set;}
}