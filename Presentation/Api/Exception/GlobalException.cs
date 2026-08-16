using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace OrderManagement.Api.Exceptions;

public class GlobalException
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _environment;
    
    public GlobalException(RequestDelegate next, IHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext http)
    {
        try
        {
            await _next(http);
        }
        catch (Exception ex)
        {
            await HandleException(http, ex);
        }
    }
    private async Task HandleException(HttpContext http, Exception exception)
    {
        if (http.Response.HasStarted)
        {
            return;
        }

        var statusCode = exception switch
        {
            KeyNotFoundException => StatusCodes.Status404NotFound,
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            InvalidOperationException => StatusCodes.Status409Conflict,
            ArgumentNullException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        var message = exception.Message;

        if (_environment.IsDevelopment() && statusCode == StatusCodes.Status500InternalServerError)
        {
            exception.ToString();
        }

        await ErrorResponseWriter.WriteAsync(http, statusCode, message, exception.GetType().Name);
    }
}