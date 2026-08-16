using OrderManagement.Application.DTO;
using OrderManagement.Application.Interface;

namespace OrderManagement.Application.Services;

public class AdminServices
{
    private readonly IAdminRepo _admin;

    public AdminServices(IAdminRepo admin)
    {
        _admin = admin;
    }

    public async Task<List<UserDTO>> GetUsers()
    {
        var userList = await _admin.GetAllUsers();

        return userList.Select(s => new UserDTO
        {
            Email = s.Email,
            isActive = s.IsActive,
            FullName = s.FullName
        }).ToList();
    }
}