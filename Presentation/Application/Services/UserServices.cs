using Microsoft.AspNetCore.Identity;
using OrderManagement.Application.DTO;
using OrderManagement.Application.Identity;
using OrderManagement.Application.Interface;

namespace OrderManagement.Application.Services;

public class UserServices
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IDataRepo _data;

    public UserServices(UserManager<ApplicationUser> userManager, IDataRepo data)
    {
        _userManager = userManager;
        _data = data;
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

    public async Task EditProfile(Guid userID, EditUserDTO edit)
    {
        var user = await _userManager.FindByIdAsync(userID.ToString()) ??
            throw new KeyNotFoundException("User with the provided ID could not be found");

        user.Email = edit.Email;
        user.UserName = edit.Username;
        user.FullName = edit.FullName;

        await _data.SaveChangesAsync();
    }
}