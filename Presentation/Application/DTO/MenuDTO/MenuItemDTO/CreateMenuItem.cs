using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.DTO.Menu.Items;

public class CreateMenuItem
{
    public string MenuItemTitle {get; set;} = string.Empty;
    public string? MenuItemDescription {get; set;}
    public Guid CategoryId {get; set;} = Guid.NewGuid();
    public Guid IngredientId {get; set;} = Guid.NewGuid();
    public ICollection<MenuCategory> Categories = new List<MenuCategory>();
    public ICollection<Ingredient> Ingredients = new List<Ingredient>();
    public decimal MenuItemPrice {get; set;}

    public bool IsAvailable {get; set;}
    public string ImageUrl {get; set;} = string.Empty;
}