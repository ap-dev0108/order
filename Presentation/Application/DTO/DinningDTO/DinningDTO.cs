using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enum;

namespace OrderManagement.Application.DTO.Dinning;

public class DinningDTO
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public Guid TableId {get; set;}
    public RestaurantTable Table {get; set;}

    public DinningStatus Status {get; set;}

    public DateTime StartedAt {get; set;} = DateTime.UtcNow;
    public DateTime EndedAt {get; set;} = DateTime.UtcNow;
}