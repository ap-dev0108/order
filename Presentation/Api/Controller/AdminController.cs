using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO;
using OrderManagement.Application.Services;

namespace OrderManagement.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly AdminServices _admin;

    public AdminController(AdminServices admin)
    {
        _admin = admin;
    }

    [HttpGet("allUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var userLists = await _admin.GetUsers();

        return Ok(new Response<List<UserDTO>>
        {
            Success = true,
            Message = "User list is fetched successfully",
            Data = userLists
        });
    }
}