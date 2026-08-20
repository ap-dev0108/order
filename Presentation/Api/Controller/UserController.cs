using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO;
using OrderManagement.Application.Services;

namespace OrderManagement.Api.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Kitchen")]
public class UserController : ControllerBase
{
    private readonly DataHelper _helper;
    private readonly IAuthenticationSchemeProvider _schema;
    private readonly UserServices _userServices;

    public UserController(DataHelper helper, UserServices userServices, IAuthenticationSchemeProvider schema)
    {
        _helper = helper;
        _userServices = userServices;
        _schema = schema;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var (userId, roles) = _helper.GetData();
        var profile = await _userServices.GetProfile(userId);

        return Ok(new Response<UserDTO>
        {
            Success = true,
            Message = "Profile API executed",
            Data = profile
        });
    }

    [HttpPatch("editUser")]
    public async Task<IActionResult> EditProfile(EditUserDTO editUserDTO, Guid id)
    {
        await _userServices.EditProfile(id, editUserDTO);

        return Ok(new Response<EditUserDTO>
        {
            Success = true,
            Message = "Edit user api fetched",
            Data = editUserDTO
        });
    }
}