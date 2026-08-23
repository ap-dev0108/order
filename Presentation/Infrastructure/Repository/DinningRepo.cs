using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repo;

public class DinningRepo : IDinningRepo
{
    private readonly AppDbContext _db;

    public DinningRepo(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<DinningSession>> GetDinningSessionsAsync()
    {
        return await _db.DinningSessions.AsNoTracking().ToListAsync();
    }

    public async Task<DinningSession> GetDinningSessionById(Guid id)
    {
        return await _db.DinningSessions.FirstOrDefaultAsync(dinning => dinning.Id == id);
    }

    public async Task AddDinning(DinningSession dinningSession)
    {
        _db.DinningSessions.Add(dinningSession);
    }
}