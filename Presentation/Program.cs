using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Identity;
using OrderManagement.Infrastructure.DI;
using OrderManagement.Infrastructure.Load;
using OrderManagement.Infrastructure.Persistence;
using OrderManagement.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<EnvLoad>();

var envLoad = new EnvLoad();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(envLoad.DbUrl));

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

RepoDI.RepoInjection(builder.Services);
ServiceDI.ServiceInjection(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    await IdentitySeed.SeedData(scope.ServiceProvider);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();