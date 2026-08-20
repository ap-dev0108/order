using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Identity;
using OrderManagement.Application.Interface;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repo;

public class AdminRepo : IAdminRepo
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminRepo(AppDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<List<ApplicationUser>> GetAllUsers()
    {
        return await _db.Users.AsNoTracking().ToListAsync();
    }

    public async Task<ApplicationUser> GetUserById(string id)
    {
        return await _db.Users.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
    }
}