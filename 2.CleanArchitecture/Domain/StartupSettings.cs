namespace Domain;

using Microsoft.Extensions.Configuration;

[ExcludeFromCodeCoverage]
public static class StartupSettings
{
    private static readonly Lazy<StartupConfiguration> CurrentValue = new(GetStartupConfiguration);

    public static StartupConfiguration Current => CurrentValue.Value;

    private static StartupConfiguration GetStartupConfiguration()
    {
        var builder = CommonConfigurationBuilder.GetCommonConfigurationBuilder();

        var config = builder.Build();
        var startupConfiguration = config.GetSection("StartupConfiguration").Get<StartupConfiguration>();

        return startupConfiguration ?? new StartupConfiguration();
    }
}
