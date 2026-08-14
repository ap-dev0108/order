namespace OrderManagement.Domain.Entities;

public class OrderItem
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