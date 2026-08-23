using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repo;

public class MenuItemRepo : IMenuItem
{
    private readonly AppDbContext _dbContext;

    public MenuItemRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<MenuItem>> GetMenuItemsAsync()
    {
        return await _dbContext.MenuItems.AsNoTracking().ToListAsync();
    }
    public async Task<MenuItem> GetMenuItemsById(Guid menuID)
    {
        return await _dbContext.MenuItems.FirstOrDefaultAsync(m => m.Id == menuID);
    }
    public async Task AddMenus(MenuItem menuItem)
    {
        _dbContext.MenuItems.Add(menuItem);
    }
    public async Task RemoveMenus(MenuItem menuItem)
    {
        _dbContext.MenuItems.Remove(menuItem);
    }
}