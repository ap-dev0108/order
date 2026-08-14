using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Domain.Entities;

public class RestaurantTable
{
    [Key]
    public Guid Id {get; set;} = Guid.NewGuid();

    public int TableNumber {get; set;}

    public string QrCodeToken {get; set;} = string.Empty;

    public int Capacity {get; set;}

    public bool IsActive {get; set;}
}