using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interface;

public interface IMenuItem
{
    Task<List<MenuItem>> GetMenuItemsAsync();
    Task<MenuItem> GetMenuItemsById(Guid menuID);

    Task AddMenus(MenuItem menuItem);

    Task RemoveMenus(MenuItem menuItem);
}