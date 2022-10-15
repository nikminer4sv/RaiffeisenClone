using Microsoft.Extensions.Configuration;

namespace RaiffeisenClone.Tests;

public static class Configuration
{
    public static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();
        return config;
    }
}