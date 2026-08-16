using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Identity;
using OrderManagement.Infrastructure.DI;
using OrderManagement.Infrastructure.Load;
using OrderManagement.Infrastructure.Persistence;
using OrderManagement.Infrastructure.Seed;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<EnvLoad>();

var envPath = Path.Combine(
    Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName,
    "Infrastructure",
    ".env"
);
Env.Load(envPath);

var envLoad = new EnvLoad();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(envLoad.DbUrl));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 4;
    opt.Password.RequiredUniqueChars = 0;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

RepoDI.RepoInjection(builder.Services);
ServiceDI.ServiceInjection(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentitySeed.SeedData(services);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();