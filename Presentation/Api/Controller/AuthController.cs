using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO;
using OrderManagement.Application.Services;

namespace OrderManagement.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthServices _authServices;

    public AuthController(AuthServices authServices)
    {
        _authServices = authServices;
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
    {
        var login = await _authServices.LoginUser(loginDTO);

        return Ok(new Response<string>
        {
            Success = true,
            Message = "Login API Executed",
            Data = login
        });
    }
}