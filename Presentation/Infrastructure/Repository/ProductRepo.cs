using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repo;

public class ProductRepo : IProductRepo
{
    private readonly AppDbContext _db;

    public ProductRepo(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Ingredient>> GetIngredientsAsync()
    {
        return await _db.Ingredients.AsNoTracking().ToListAsync();
    }

    public async Task<Ingredient> GetIngredientById(Guid id)
    {
        return await _db.Ingredients.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task AddIngredients(Ingredient ingredient)
    {
        _db.Ingredients.Add(ingredient);
    }

    public async Task RemoveIngredients(Ingredient ingredient)
    {
        _db.Ingredients.Remove(ingredient);
    }
}