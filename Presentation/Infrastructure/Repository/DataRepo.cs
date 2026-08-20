using OrderManagement.Application.Interface;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repo;

public class DataRepo : IDataRepo
{
    private readonly AppDbContext _db;

    public DataRepo(AppDbContext db)
    {
        _db = db;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}