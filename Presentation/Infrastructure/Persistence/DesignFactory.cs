using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OrderManagement.Infrastructure.Load;
using DotNetEnv;

namespace OrderManagement.Infrastructure.Persistence.DesignTime;

public class DesignFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        Env.Load();
        var env = new EnvLoad();
        var optionBuilders = new DbContextOptionsBuilder<AppDbContext>();
        optionBuilders.UseNpgsql(env.DbUrl);

        return new AppDbContext(optionBuilders.Options);
    }
}