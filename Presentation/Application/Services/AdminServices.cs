using Microsoft.AspNetCore.Identity;
using OrderManagement.Application.DTO;
using OrderManagement.Application.Identity;
using OrderManagement.Application.Interface;

namespace OrderManagement.Application.Services;

public class AdminServices
{
    private readonly IAdminRepo _admin;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminServices(IAdminRepo admin, UserManager<ApplicationUser> userManager)
    {
        _admin = admin;
        _userManager = userManager;
    }

    public async Task<List<UserDTO>> GetUsers()
    {
        var userList = await _admin.GetAllUsers();

        return userList.Select(s => new UserDTO
        {
            Id = s.Id,
            Email = s.Email,
            isActive = s.IsActive,
            FullName = s.FullName
        }).ToList();
    }

    public async Task<UserDTO> GetUsersById(string id)
    {
        var user = await _userManager.FindByIdAsync(id) ??
            throw new KeyNotFoundException("User with the ID cannot be found");

        return new UserDTO
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            isActive = user.IsActive
        };
    }
}