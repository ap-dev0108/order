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

    public MenuItemServices(IMenuItem menu, IDataRepo data)
    {
        _menu = menu;
        _data = data;
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
            CategoryId = s.CategoryId,
            MenuItemDescription = s.MenuItemDescription,
            MenuItemPrice = s.MenuItemPrice,
            ImageUrl = s.ImageUrl,
            IngredientId = s.IngredientId,
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
            CategoryId = MenuById.CategoryId,
            MenuItemDescription = MenuById.MenuItemDescription,
            MenuItemPrice = MenuById.MenuItemPrice,
            ImageUrl = MenuById.ImageUrl,
            IngredientId = MenuById.IngredientId,
            Ingredients = MenuById.Ingredients,
            IsAvailable = MenuById.IsAvailable
        };
    }

    public async Task AddMenuItem(CreateMenuItem createMenuItem)
    {
        var MenuToAdd = new MenuItem
        {
            MenuItemTitle = createMenuItem.MenuItemTitle,
            Category = createMenuItem.Category,
            CategoryId = createMenuItem.CategoryId,
            MenuItemDescription = createMenuItem.MenuItemDescription,
            MenuItemPrice = createMenuItem.MenuItemPrice,
            ImageUrl = createMenuItem.ImageUrl,
            IngredientId = createMenuItem.IngredientId,
            Ingredients = createMenuItem.Ingredients,
            IsAvailable = createMenuItem.IsAvailable     
        };

        await _menu.AddMenus(MenuToAdd);
        await _data.SaveChangesAsync();
    }

    public async Task EditMenuItems(Guid menuID, EditMenuItem editMenuItem)
    {
        var MenuTOEdit = await _menu.GetMenuItemsById(menuID) ??
            throw new KeyNotFoundException("Menu with the given ID cannot be found");

        MenuTOEdit.CategoryId = editMenuItem.CategoryId;
        MenuTOEdit.Category = editMenuItem.Category;
        MenuTOEdit.ImageUrl = editMenuItem.ImageUrl;
        MenuTOEdit.IngredientId = editMenuItem.IngredientId;
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