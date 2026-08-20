using OrderManagement.Application.Identity;

namespace OrderManagement.Application.Interface;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(ApplicationUser user);
}