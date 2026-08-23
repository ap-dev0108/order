using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repo;

public class MenuCategoryRepo : IMenuCategoryRepo
{
    private readonly AppDbContext _db;

    public MenuCategoryRepo(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<MenuCategory>> GetMenuCategoriesAsync(bool ascending = true)
    {
        var query = _db.MenuCategories.AsQueryable();
        query = ascending ? query.OrderBy(c => c.DisplayOrder) : query.OrderByDescending(c => c.DisplayOrder);

        return await query.ToListAsync();
    }

    public async Task<MenuCategory> MenuCategoryByIdAsync(Guid id)
    {
        return await _db.MenuCategories.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task AddMenuCategory(MenuCategory menuCategory)
    {
        _db.MenuCategories.Add(menuCategory);
    }

    public async Task RemoveMenuCategory(MenuCategory menuCategory)
    {
        _db.MenuCategories.Remove(menuCategory);
    }
}