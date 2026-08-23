using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interface;

public interface IMenuCategoryRepo
{
    Task<List<MenuCategory>> GetMenuCategoriesAsync();
    Task<MenuCategory> MenuCategoryByIdAsync(Guid id);

    Task AddMenuCategory(MenuCategory menuCategory);

    Task RemoveMenuCategory(MenuCategory menuCategory);
}