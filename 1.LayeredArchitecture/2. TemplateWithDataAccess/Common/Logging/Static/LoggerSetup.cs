namespace Common.Logging.Static;

using Common;
using Common.Logging.Config;
using Serilog;
using Serilog.Enrichers.Correlate;
using Serilog.Events;

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
            .Enrich.WithProperty("thread_id", Thread.CurrentThread.ManagedThreadId.ToString())
            .Enrich.WithProperty("process_id", Process.GetCurrentProcess().Id.ToString())

            // Can this be null?
            // .Enrich.WithProperty("process_user_id", WindowsIdentity.GetCurrent().Name)
            .Enrich.WithProperty("correlation_id", "To Be Confirmed")
            .Enrich.WithProperty("site_name", LoggingConfig.Current.SolutionName)

            // Can this be null?
            // .Enrich.WithProperty("user_id", WindowsIdentity.GetCurrent().Name)
            .MinimalLevel.Information()
            .MinimalLevel.Override("System", LogEventLevel.Error)
            .MinimalLevel.Override("Microsoft", LogEventLevel.Error);

        if (LoggingConfig.Current.IncludeDebugLogs)
        {
            LoggingConfiguration.MinimalLevel.Debug();
        }

        if (LoggingConfig.Current.IncludeSystemInfoLogs)
        {
            LoggingConfiguration.MinimalLevel.Override("System", LogEventLevel.Information);
            LoggingConfiguration.MinimalLevel.Override("Microsoft", LogEventLevel.Information);
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

    public static int NumberOfMegabytes(int number) => number * 1024 * 1024;
}
