using OrderManagement.Application.Identity;

namespace OrderManagement.Application.Interface;

public interface IAdminRepo
{
    Task<List<ApplicationUser>> GetAllUsers();
}