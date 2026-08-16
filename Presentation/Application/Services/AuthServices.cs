using OrderManagement.Application.DTO;
using OrderManagement.Application.Identity;
using OrderManagement.Application.Interface;

namespace OrderManagement.Application.Services;

public class AuthServices
{
    private readonly IAuthRepo _authRepo;

    public AuthServices(IAuthRepo authRepo)
    {
        _authRepo = authRepo;
    }

    public async Task<string> LoginUser(LoginDTO loginDTO)
    {
        var user = new ApplicationUser
        {
            Email = loginDTO.Email,
            PasswordHash = loginDTO.Password  
        };

        var loginUser = await _authRepo.LoginUser(user);
        if (!loginUser.Succeeded)
        {
            throw new Exception("User login failed");
        }

        return "Login Done";
    }
}