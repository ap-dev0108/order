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


    [HttpGet("auth-test")]
    public IActionResult AuthTest()
    {
        return Ok(new
        {
            IsAuthenticated = User.Identity?.IsAuthenticated,
            AuthenticationType = User.Identity?.AuthenticationType,
            Name = User.Identity?.Name,
            Claims = User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            })
        });
    }

    [HttpGet("test")]
    public async Task<IActionResult> Auth()
    {
        var defaultAuthenticate = await _schema.GetDefaultAuthenticateSchemeAsync();

        var defaultChallenge =
            await _schema.GetDefaultChallengeSchemeAsync();

        var defaultScheme =
            await _schema.GetDefaultSignInSchemeAsync();

        return Ok(new
        {
            DefaultScheme = defaultScheme?.Name,
            DefaultAuthenticateScheme = defaultAuthenticate?.Name,
            DefaultChallengeScheme = defaultChallenge?.Name
        });
    }

    [HttpGet("auth-debug")]
    public IActionResult AuthDebug()
    {
        var authHeader = Request.Headers.Authorization.ToString();

        return Ok(new
        {
            AuthorizationHeader = authHeader,
            IsAuthenticated = User.Identity?.IsAuthenticated,
            AuthenticationType = User.Identity?.AuthenticationType
        });
    }
}