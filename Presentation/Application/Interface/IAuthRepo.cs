using Microsoft.AspNetCore.Identity;
using OrderManagement.Application.Identity;

namespace OrderManagement.Application.Interface;

public interface IAuthRepo
{
    Task<SignInResult> LoginUser(ApplicationUser user);
}