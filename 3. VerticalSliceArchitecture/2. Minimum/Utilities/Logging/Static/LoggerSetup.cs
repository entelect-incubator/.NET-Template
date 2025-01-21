namespace Utilities.Logging.Static;

using System.Globalization;
using Serilog;
using Serilog.Enrichers.Correlate;
using Serilog.Events;
using Utilities.Logging.Config;

[ExcludeFromCodeCoverage]
public static class LoggerSetup
{
    public static LoggerConfiguration LoggingConfiguration;

    public static LoggerConfiguration ConfigureLogging(IServiceProvider? services = null)
    {
        if (LoggingConfiguration != null)
        {
            return LoggingConfiguration;
        }

        LoggingConfiguration = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("SolutionName", LoggingConfig.Current.SolutionName)
            .Enrich.WithProperty("Environment", LoggingConfig.Current.Environment)
            .Enrich.WithProperty("logger_name", LoggingConfig.Current.SolutionName)
            .Enrich.WithProperty("machine_name", Environment.MachineName)
            .Enrich.WithProperty("application_name", LoggingConfig.Current.SolutionName)
            .Enrich.WithProperty("thread_id", Environment.CurrentManagedThreadId.ToString())
            .Enrich.WithProperty("process_id", Environment.ProcessId.ToString())
            .Enrich.WithProperty("site_name", LoggingConfig.Current.SolutionName)

            // Can this be null?
            // .Enrich.WithProperty("user_id", WindowsIdentity.GetCurrent().Name)
            .MinimumLevel.Information()
            .MinimumLevel.Override("System", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error);

        if (LoggingConfig.Current.IncludeDebugLogs)
        {
            LoggingConfiguration.MinimumLevel.Debug();
        }

        if (LoggingConfig.Current.IncludeSystemInfoLogs)
        {
            LoggingConfiguration.MinimumLevel.Override("System", LogEventLevel.Information);
            LoggingConfiguration.MinimumLevel.Override("Microsoft", LogEventLevel.Information);
        }

        if (StartupSettings.Current.IncludeCorrelationId)
        {
            if (services != null)
            {
                LoggingConfiguration.Enrich.WithCorrelate(services);
            }
        }

        if (LoggingConfig.Current.IncludeWriteToFile)
        {
            LoggingConfiguration.ConfigureFileLoggingSink();
        }

        LoggingConfiguration.ConfigureConsoleSink();
        LoggingConfiguration.ConfigureOpenTelemetrySink();

        return LoggingConfiguration;
    }

    public static LoggerConfiguration ConfigureFileLoggingSink(this LoggerConfiguration configuration)
    {
        var filePath = Path.Combine(LoggingConfig.Current.FilePath, LoggingConfig.Current.SolutionName, ".log");
        if (!File.Exists(filePath))
        {
            var relativePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            relativePath = Path.Combine(relativePath, LoggingConfig.Current.FilePath);
            if (!Directory.Exists(relativePath))
            {
                Directory.CreateDirectory(relativePath);
            }

            filePath = Path.Combine(relativePath, ".log");
        }

        return configuration.BaseWriteFile(filePath);
    }

    public static LoggerConfiguration BaseWriteFile(this LoggerConfiguration configuration, string path)
    {
        var filePath = path;

        configuration.WriteTo.Async(a => a.File(
            path: filePath,
            buffered: true,
            flushToDiskInterval: TimeSpan.FromSeconds(10),
            rollOnFileSizeLimit: true,
            retainedFileCountLimit: null,
            rollingInterval: RollingInterval.Day,
            fileSizeLimitBytes: NumberOfMegabytes(10),
            formatProvider: CultureInfo.InvariantCulture,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}|[{Level:u3}]|{Message:lj}{NewLine}{Exception}"));

        return configuration;
    }

    public static LoggerConfiguration ConfigureOpenTelemetrySink(this LoggerConfiguration configuration)
    {
        configuration.WriteTo.OpenTelemetry(options =>
        {
            options.Endpoint = Settings.Current.OpenTelemetryExportUrl;
            options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.HttpProtobuf;
        });

        return configuration;
    }

    public static LoggerConfiguration ConfigureConsoleSink(this LoggerConfiguration configuration)
    {
        configuration.WriteTo.Console();

        return configuration;
    }

    public static int NumberOfMegabytes(int number) => number * 1024 * 1024;
}
