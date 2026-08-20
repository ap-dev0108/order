using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using OrderManagement.Application.DTO;
using OrderManagement.Application.Identity;
using OrderManagement.Application.Interface;

namespace OrderManagement.Application.Services;

public class AuthServices
{
    private readonly IAuthRepo _authRepo;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _token;

    public AuthServices(IAuthRepo authRepo, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService token)
    {
        _userManager = userManager;
        _authRepo = authRepo;
        _roleManager = roleManager;
        _token = token;
    }

    public async Task<string> LoginUser(LoginDTO loginDTO)
    {
        var userExists = await _userManager.FindByEmailAsync(loginDTO.Email);

        if (userExists?.Email is null)
            return "The provided usermail cannot be found at all";

        var loginUser = await _authRepo.LoginUser(userExists, loginDTO.Password);
        if (!loginUser.Succeeded)
        {
            throw new Exception("User login failed");
        }

        return await _token.GenerateTokenAsync(userExists);
    }

    public async Task<RegisterDTO> RegisterUser(RegisterDTO registerDTO)
    {
        var newUser = new ApplicationUser
        {
            FullName = registerDTO.Name,
            Email = registerDTO.Email,
            UserName = registerDTO.Name,
            IsActive = true,
            Roles = registerDTO.Role
        };

        var userExists = await _userManager.FindByEmailAsync(registerDTO.Email);

        if (userExists != null)
        {
            throw new InvalidOperationException("User with this email already exists. Please login");
        }

        if (!await _roleManager.RoleExistsAsync(registerDTO.Role))
        {
            throw new KeyNotFoundException($"The role: {registerDTO.Role} cannot be found.");
        }

        if (registerDTO.Role == "Admin")
        {
            throw new InvalidOperationException($"You are not allowed to register as an admin");
        }

        var userRegistration = await _userManager.CreateAsync(newUser, registerDTO.Password);

        if (!userRegistration.Succeeded)
        {
            foreach (var error in userRegistration.Errors)
            {
                throw new Exception($"Error while registering user: {error.Description}");
            }
        }

        await _userManager.AddToRoleAsync(newUser, registerDTO.Role);

        return registerDTO;
    }

}