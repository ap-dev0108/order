using OrderManagement.Application.Identity;

namespace OrderManagement.Application.Interface;

public interface ITokenService
{
    string GenerateTokenAsync(ApplicationUser user);
}