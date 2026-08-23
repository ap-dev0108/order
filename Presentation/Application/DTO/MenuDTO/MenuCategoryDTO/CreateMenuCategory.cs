namespace OrderManagement.Application.DTO.Menu.Category;

public class CreateMenuCategory
{
    public string MenuCategoryTitle {get; set;} = string.Empty;
    public int DisplayOrder {get; set;}
}