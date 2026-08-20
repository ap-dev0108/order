using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OrderManagement.Application.Identity;
using OrderManagement.Application.Interface;

namespace OrderManagement.Application.Services;

public class DataHelper : IDataHelper
{
    private readonly IHttpContextAccessor _http;

    public DataHelper(IHttpContextAccessor http)
    {
        _http = http;
    }
    public (string userId, string role) GetData()
    {
        var httpContext = _http.HttpContext ??
            throw new InvalidOperationException("HttpContext is null");

        if (httpContext.User.Identity?.IsAuthenticated != true)
        {
            throw new UnauthorizedAccessException(
                "The current request is not authenticated."
            );
        }

        var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? string.Empty;

        var roles = httpContext.User.FindAll(ClaimTypes.Role)
            .Select(x => x.Value)
            .DefaultIfEmpty(string.Empty)
            .ToArray();

        var roleString = string.Join(",", roles.Where(r => !string.IsNullOrWhiteSpace(r)));

        return new(userId, roleString);
    }
}