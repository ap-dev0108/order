using System.Linq.Expressions;
using OrderManagement.Infrastructure.Persistence;
using OrderManagement.Application.Interface;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Infrastructure.Repo;

public class Searchable
{
    private readonly AppDbContext _db;

    public Searchable(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<T>> SearchByNameAsync<T>(string term) where T : class, ISerachable
    {
        return await _db.Set<T>().Where(x => x.Name.Contains(term)).ToListAsync();
    }
}