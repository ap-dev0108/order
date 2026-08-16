using System.Text.Json;
using Microsoft.AspNetCore.Http;
using OrderManagement.Application.DTO;

namespace OrderManagement.Api.Exceptions;

public static class ErrorResponseWriter
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public static async Task WriteAsync(
        HttpContext http,
        int statusCode,
        string message,
        string? error = null)
    {
        if (http.Response.HasStarted)
        {
            return;
        }

        http.Response.ContentType = "application/json";
        http.Response.StatusCode = statusCode;

        var response = new Response<object?>
        {
            Success = false,
            Message = message,
            Data = new
            {
                error,
                traceId = http.TraceIdentifier,
                path = http.Request.Path.Value
            }
        };

        await http.Response.WriteAsync(JsonSerializer.Serialize(response, JsonOptions));
    }
}