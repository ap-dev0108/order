namespace OrderManagement.Domain.Entities;

public class MenuCategory
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public string MenuCategoryTitle {get; set;} = string.Empty;
    public int DisplayOrder {get; set;}
}