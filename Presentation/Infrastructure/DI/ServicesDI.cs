using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application.Interface;
using OrderManagement.Application.Services;

namespace OrderManagement.Infrastructure.DI;

public class ServiceDI
{
    public static IServiceCollection ServiceInjection(IServiceCollection services)
    {
        services.AddScoped<AuthServices>();
        services.AddScoped<AdminServices>();
        services.AddScoped<UserServices>();
        services.AddScoped<DataHelper>();
        services.AddScoped<ProductServices>();
        services.AddScoped<DinningServices>();
        services.AddScoped<MenuCategoryService>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<TokenService>();

        return services;
    }
}