using Microsoft.Extensions.Configuration;

namespace Utilities.Helpers;

public class ConfigurationHelper
{
    private static IConfiguration ConfigBuild(string file)
    {
        return new Microsoft.Extensions.Configuration.ConfigurationBuilder()
            .AddJsonFile(file)
            .AddEnvironmentVariables()
            .Build();
    }
    public static T Create<T>(string key, string file = "appsettings.json")
    {
        IConfiguration config = ConfigBuild(file);
        return config.GetRequiredSection(key).Get<T>();
    }

    public static IConfigurationSection Create(string key, string file = "appsettings.json")
    {
        IConfiguration config = ConfigBuild(file);
        return config.GetSection(key);
    }
}