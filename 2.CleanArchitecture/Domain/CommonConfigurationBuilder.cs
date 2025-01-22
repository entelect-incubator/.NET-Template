namespace Domain;

using Microsoft.Extensions.Configuration;

public class CommonConfigurationBuilder
{
    public static IConfigurationBuilder GetCommonConfigurationBuilder()
    {
        // This environment variable is set in kubernetes so that the appsettings and appsecrets can be stored in mounted volumes
        var configFileBasePath = Environment.GetEnvironmentVariable("CONFIG_BASEPATH");
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsecrets.json", optional: true)
            .AddJsonFile("settings/appsettings.json", optional: true) // This is the deployed appsettings path
            .AddJsonFile("secrets/appsecrets.json", optional: true) // This is the deployed appsecrets path
            .AddEnvironmentVariables();

        // If CONFIG_BASEPATH is not set, the appsettings from the root will be used
        if (!string.IsNullOrEmpty(configFileBasePath))
        {
            configurationBuilder.SetBasePath(configFileBasePath);
        }

        return configurationBuilder;
    }
}
