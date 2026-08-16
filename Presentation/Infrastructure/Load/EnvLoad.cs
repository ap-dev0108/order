namespace OrderManagement.Infrastructure.Load;

public class EnvLoad
{
    public string DbUrl { get; set; } = string.Empty;
    public string AdminEmail { get; set; } = string.Empty;
    public string AdminPassword { get; set; } = string.Empty;

    public EnvLoad()
    {
        DbUrl = GetRequired("DATABASE_URL");
        AdminEmail = GetRequired("ADMIN_EMAIL");
        AdminPassword = GetRequired("ADMIN_PASSWORD");
    }

    public static string GetRequired(string key)
    {
        return Environment.GetEnvironmentVariable(key) ??
            throw new KeyNotFoundException(
                $"Provided key variable for {key} cannot be found"
            );
    }
}