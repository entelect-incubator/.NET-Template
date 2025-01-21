namespace Utilities.Logging.Static;

public interface ILoggerWrapper
{
    void LogInfo(string subject, string message, object? logObject = null);

    void LogDebug(string subject, string message, object? logObject = null);

    void LogException(string ex, object? logObject = null);

    void LogException(Exception ex, object? logObject = null);
}
