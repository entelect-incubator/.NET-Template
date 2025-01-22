namespace Common;

using Microsoft.Extensions.Configuration;

[ExcludeFromCodeCoverage]
public static class Settings
{
    private static readonly Lazy<SettingsConfiguration> CurrentValue = new(GetSettingsConfiguration);

    public static SettingsConfiguration Current => CurrentValue.Value;

    private static SettingsConfiguration GetSettingsConfiguration()
    {
        var builder = CommonConfigurationBuilder.GetCommonConfigurationBuilder();

        var config = builder.Build();
        var configuration = config.GetSection("Settings").Get<SettingsConfiguration>();

        return configuration ?? new SettingsConfiguration();
    }
}
