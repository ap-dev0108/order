using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Identity;
using OrderManagement.Application.Interface;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repo;

public class AdminRepo : IAdminRepo
{
    private readonly AppDbContext _db;

    public AdminRepo(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<ApplicationUser>> GetAllUsers()
    {
        return await _db.Users.AsNoTracking().ToListAsync();
    }
}