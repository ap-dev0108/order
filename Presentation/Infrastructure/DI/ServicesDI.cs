using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application.Services;

namespace OrderManagement.Infrastructure.DI;

public class ServiceDI
{
    public static IServiceCollection ServiceInjection(IServiceCollection services)
    {
        services.AddScoped<AuthServices>();
        services.AddScoped<AdminServices>();

        return services;
    }
}