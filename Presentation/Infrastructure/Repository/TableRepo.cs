using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repo;

public class TableRepo : ITableRepo
{
    private readonly AppDbContext _db;

    public TableRepo(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<RestaurantTable>> GetRestaurantTablesAsync()
    {
        return await _db.RestaurantTables.AsNoTracking().ToListAsync();
    }

    public async Task<RestaurantTable> GetRestaurantTableByIdAsync(Guid id)
    {
        return await _db.RestaurantTables.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task AddRestaurantTable(RestaurantTable restaurantTable)
    {
        _db.RestaurantTables.Add(restaurantTable);
    }

    public async Task RemoveTable(RestaurantTable restaurantTable)
    {
        _db.RestaurantTables.Remove(restaurantTable);
    }
}