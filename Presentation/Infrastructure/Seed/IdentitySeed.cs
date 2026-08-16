using Microsoft.AspNetCore.Identity;
using OrderManagement.Application.Identity;

namespace OrderManagement.Infrastructure.Seed;

public static class IdentitySeed
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = {"Management", "Kitchen", "Admin"};

        foreach(var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }

        var adminEmail = "aryan@order.app";
        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var admin = new ApplicationUser
            {
                UserName = "Aryan",
                Email = adminEmail,
                IsActive = true
            };

            var result = await userManager.CreateAsync(admin, "order@now");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}