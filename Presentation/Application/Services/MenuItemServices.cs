using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OrderManagement.Application.DTO.Menu.Items;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Services;

public class MenuItemServices
{
    private readonly IMenuItem _menu;
    private readonly IDataRepo _data;
    private readonly ISearchable _searchable;
    private readonly IProductRepo _ingredients;
    private readonly IMenuCategoryRepo _category;

    public MenuItemServices(IMenuItem menu, IDataRepo data, ISearchable searchable, IProductRepo ingredients, IMenuCategoryRepo category)
    {
        _menu = menu;
        _data = data;
        _searchable = searchable;
        _category = category;
        _ingredients = ingredients;
    }

    public async Task<List<DisplayMenuItem>> GetMenuItemsAsync()
    {
        var MenuList = await _menu.GetMenuItemsAsync() ??
            throw new KeyNotFoundException("Menu not found");

        return MenuList.Select(s => new DisplayMenuItem
        {
            Id = s.Id,
            MenuItemTitle = s.MenuItemTitle,
            Category = s.Category,
            MenuItemDescription = s.MenuItemDescription,
            MenuItemPrice = s.MenuItemPrice,
            ImageUrl = s.ImageUrl,
            Ingredients = s.Ingredients,
            IsAvailable = s.IsAvailable
        }).ToList();
    }

    public async Task<DisplayMenuItem> GetMenuItemById(Guid menuID)
    {
        var MenuById = await _menu.GetMenuItemsById(menuID) ??
            throw new KeyNotFoundException("Menu with the given ID cannot be found");

        return new DisplayMenuItem
        {
            Id = MenuById.Id,
            MenuItemTitle = MenuById.MenuItemTitle,
            Category = MenuById.Category,
            MenuItemDescription = MenuById.MenuItemDescription,
            MenuItemPrice = MenuById.MenuItemPrice,
            ImageUrl = MenuById.ImageUrl,
            Ingredients = MenuById.Ingredients,
            IsAvailable = MenuById.IsAvailable
        };
    }

    public async Task AddMenuItem(Guid categoryId, Guid ingredientId, CreateMenuItem createMenuItem)
    {
        var categoryExists = await _category.MenuCategoryByIdAsync(categoryId) ??
            throw new KeyNotFoundException("Category not found");

        var ingredientExists = await _ingredients.GetIngredientById(ingredientId) ??
            throw new KeyNotFoundException("Ingredient not found");

        var MenuToAdd = new MenuItem
        {
            MenuItemTitle = createMenuItem.MenuItemTitle,
            Category = categoryExists,
            MenuItemDescription = createMenuItem.MenuItemDescription,
            MenuItemPrice = createMenuItem.MenuItemPrice,
            ImageUrl = createMenuItem.ImageUrl,
            IsAvailable = createMenuItem.IsAvailable
        };

        MenuToAdd.Ingredients.Add(ingredientExists);

        var MenuTitleExists = await _searchable.SearchByNameAsync<MenuItem>(m => m.MenuItemTitle == createMenuItem.MenuItemTitle);
        if (MenuTitleExists.Any())
        {
            throw new InvalidOperationException("Menu title already exists");
        }

        await _menu.AddMenus(MenuToAdd);
        await _data.SaveChangesAsync();
    }

    public async Task EditMenuItems(Guid menuID, EditMenuItem editMenuItem)
    {
        var MenuTOEdit = await _menu.GetMenuItemsById(menuID) ??
            throw new KeyNotFoundException("Menu with the given ID cannot be found");

        MenuTOEdit.Category = editMenuItem.Category;
        MenuTOEdit.ImageUrl = editMenuItem.ImageUrl;
        MenuTOEdit.Ingredients = editMenuItem.Ingredients;
        MenuTOEdit.IsAvailable = editMenuItem.IsAvailable;
        MenuTOEdit.MenuItemDescription = editMenuItem.MenuItemDescription;
        MenuTOEdit.MenuItemPrice = editMenuItem.MenuItemPrice;
        MenuTOEdit.MenuItemTitle = editMenuItem.MenuItemTitle;

        await _data.SaveChangesAsync();
    }

    public async Task RemoveMenuItem(Guid menuID)
    {
        var MenuToRemove = await _menu.GetMenuItemsById(menuID) ??
            throw new KeyNotFoundException("Menu with the given ID cannot be found");

        await _menu.RemoveMenus(MenuToRemove);
        await _data.SaveChangesAsync();
    }
}