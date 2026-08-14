namespace OrderManagement.Infrastructure.Load;

public class EnvLoad
{
    public string DbUrl { get; set; } = string.Empty;

    public EnvLoad()
    {
        DbUrl = GetRequired("DATABASE_URL");
    }

    public static string GetRequired(string key)
    {
        return Environment.GetEnvironmentVariable(key) ??
            throw new KeyNotFoundException(
                "Provided key variable cannot be found"
            );
    }
}