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

        services.AddScoped<IDataRepo, DataRepo>();
        services.AddScoped<DataRepo>();

        services.AddScoped<IProductRepo, ProductRepo>();
        services.AddScoped<ProductRepo>();

        services.AddScoped<ITableRepo, TableRepo>();
        services.AddScoped<TableRepo>();

        services.AddScoped<IDinningRepo, DinningRepo>();
        services.AddScoped<DinningRepo>();

        services.AddScoped<IMenuCategoryRepo, MenuCategoryRepo>();
        services.AddScoped<MenuCategoryRepo>();

        services.AddScoped<IMenuItem, MenuItemRepo>();
        services.AddScoped<MenuItemRepo>();

        services.AddScoped<Searchable>();

        return services;
    }
}