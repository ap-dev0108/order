using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enum;

namespace OrderManagement.Application.DTO.Products;

public class AddProductsDTO
{
    public string Title {get; set;} = string.Empty;
    public Unit Units {get; set;}
    public decimal QualityOnHand {get; set;}
    public decimal ReorderThreshold {get; set;}
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}

public class AddInventoryTransaction
{
    public Guid IngredientId {get; set;}
    public Ingredient Ingredients {get; set;}

    public decimal ChargeAmount {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
}