using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Domain.Entities;

public class MenuItem
{
    [Key]
    public Guid Id {get; set;} = Guid.NewGuid();

    public Guid CategoryId {get; set;}
    public ICollection<MenuCategory> Category {get; set;}

    public Guid IngredientId {get; set;}
    public ICollection<Ingredient> Ingredients {get; set;}

    public string MenuItemTitle {get; set;} = string.Empty;
    public string? MenuItemDescription {get; set;}

    public decimal MenuItemPrice {get; set;}

    public bool IsAvailable {get; set;}
    public string ImageUrl {get; set;} = string.Empty;
}