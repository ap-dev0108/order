using OrderManagement.Application.DTO.Menu.Category;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Services;

public class MenuCategoryService
{
    private readonly IMenuCategoryRepo _menuCategory;
    private readonly IDataRepo _data;
    private readonly ISearchable _search;

    public MenuCategoryService(IMenuCategoryRepo menuCategoryRepo, IDataRepo data, ISearchable search)
    {
        _menuCategory = menuCategoryRepo;
        _data = data;
        _search = search;
    }

    public async Task<List<DisplayMenuCategory>> GetMenuCategoriesAsync()
    {
        var menuCategoriesList = await _menuCategory.GetMenuCategoriesAsync();

        return menuCategoriesList.Select(s => new DisplayMenuCategory
        {
            Id = s.Id,
            MenuCategoryTitle = s.MenuCategoryTitle,
        }).ToList();
    }

    public async Task<DisplayMenuCategory> GetMenuCategoryById(Guid categoryID)
    {
        var menuToDisplay = await _menuCategory.MenuCategoryByIdAsync(categoryID);

        return new DisplayMenuCategory
        {
            Id = menuToDisplay.Id,
            MenuCategoryTitle = menuToDisplay.MenuCategoryTitle
        };
    }

    public async Task AddMenuCategory(CreateMenuCategory createMenuCategory)
    {
        var CategoryToAdd = new MenuCategory
        {
            MenuCategoryTitle = createMenuCategory.MenuCategoryTitle.ToLower(),
            DisplayOrder = createMenuCategory.DisplayOrder
        };

        var CategoryExists = await _search.SearchByNameAsync<MenuCategory>(m => m.MenuCategoryTitle.Contains(createMenuCategory.MenuCategoryTitle));
        var CategoryOrderExists = await _search.SearchByNameAsync<MenuCategory>(m => m.DisplayOrder == createMenuCategory.DisplayOrder);

        if (CategoryExists.Any() && CategoryOrderExists.Any())
        {
            throw new InvalidOperationException("Category Title or Order already exists");
        }

        await _menuCategory.AddMenuCategory(CategoryToAdd);
        await _data.SaveChangesAsync();
    }

    public async Task RemoveMenuCategory(Guid categoryID)
    {
        var CategoryToRemove = await _menuCategory.MenuCategoryByIdAsync(categoryID) ??
            throw new KeyNotFoundException("Category cannot be found");

        await _menuCategory.RemoveMenuCategory(CategoryToRemove);
        await _data.SaveChangesAsync();
    }

    public async Task EditCategory(Guid categoryID, EditMenuCategory editMenuCategory)
    {
        var CategoryToEdit = await _menuCategory.MenuCategoryByIdAsync(categoryID) ??
            throw new KeyNotFoundException("Category cannot be found");

        CategoryToEdit.MenuCategoryTitle = editMenuCategory.MenuCategoryTitle;
        CategoryToEdit.DisplayOrder = editMenuCategory.DisplayOrder;

        await _data.SaveChangesAsync();
    }

    public async Task RemoveCategory(Guid categoryID)
    {
        var CategoryToRemove = await _menuCategory.MenuCategoryByIdAsync(categoryID) ??
            throw new KeyNotFoundException("Category cannot be found");

        await _menuCategory.RemoveMenuCategory(CategoryToRemove);
        await _data.SaveChangesAsync();
    }
}