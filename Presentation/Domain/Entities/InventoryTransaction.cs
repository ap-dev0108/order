namespace OrderManagement.Domain.Entities;

public class InventoryTransaction
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public Guid IngredientId {get; set;}
    public Ingredient Ingredients {get; set;}

    public decimal ChargeAmount {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
}