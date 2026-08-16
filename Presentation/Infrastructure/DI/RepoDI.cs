using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application.Interface;
using OrderManagement.Infrastructure.Repo;

namespace OrderManagement.Infrastructure.DI;

public class RepoDI
{
    public static IServiceCollection RepoInjection(IServiceCollection services)
    {
        services.AddScoped<IAuthRepo, AuthRepo>();
        services.AddScoped<AuthRepo>();

        services.AddScoped<IAdminRepo, AdminRepo>();
        services.AddScoped<AdminRepo>();

        return services;
    }
}