using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application.Identity;
using OrderManagement.Infrastructure.Load;

namespace OrderManagement.Infrastructure.Seed;

public static class IdentitySeed
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        var local = new EnvLoad();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = { "Management", "Kitchen", "Admin" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminEmail = local.AdminEmail;
        if (await userManager.FindByEmailAsync(adminEmail) == null || adminEmail.ToString() == null)
        {
            var admin = new ApplicationUser
            {
                FullName = "Aryan",
                UserName = "Aryan",
                Email = adminEmail,
                IsActive = true
            };

            var result = await userManager.CreateAsync(admin, local.AdminPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception($"Admin registration could not be done due to the following reasons: {error.Description}");
                }
            }
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}