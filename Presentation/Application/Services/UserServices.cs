using Microsoft.AspNetCore.Identity;
using OrderManagement.Application.DTO;
using OrderManagement.Application.Identity;

namespace OrderManagement.Application.Services;

public class UserServices
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserServices(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserDTO> GetProfile(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId) ??
            throw new KeyNotFoundException($"User with the given ID: {userId} cannot be found");

        var profile = new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            isActive = user.IsActive
        };

        return profile;
    }
}