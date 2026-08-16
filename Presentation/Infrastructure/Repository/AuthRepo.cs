using Microsoft.AspNetCore.Identity;
using OrderManagement.Application.Identity;
using OrderManagement.Application.Interface;

namespace OrderManagement.Infrastructure.Repo;

public class AuthRepo : IAuthRepo
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthRepo(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<SignInResult> LoginUser(ApplicationUser user, string passowrd)
    {
        Console.WriteLine($"AuthRepo is getting the following data: {user}, {passowrd}");
        return await _signInManager.CheckPasswordSignInAsync(user, passowrd, false);
    }
}