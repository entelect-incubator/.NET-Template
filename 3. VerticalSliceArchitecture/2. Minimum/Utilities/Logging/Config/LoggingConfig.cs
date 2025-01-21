namespace Utilities.Logging.Config;

[ExcludeFromCodeCoverage]
public static class LoggingConfig
{
    private static readonly Lazy<LoggingConfiguration> CurrentValue = new(GetLoggingConfiguration);

    public static LoggingConfiguration Current => CurrentValue.Value;

    private static LoggingConfiguration GetLoggingConfiguration()
    {
        var builder = CommonConfigurationBuilder.GetCommonConfigurationBuilder();

        var config = builder.Build();
        var loggingConfiguration = config.GetSection("LoggingConfiguration").Get<LoggingConfiguration>();

        return loggingConfiguration ?? new LoggingConfiguration();
    }
}
