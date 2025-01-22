namespace Common.Logging.Static;

using System.Diagnostics;
using Serilog;
using Serilog.Context;

[ExcludeFromCodeCoverage]
public static class Logger
{
    private const string InfoMessageTemplate = "{Subject:l}|{LogMessage:l}|{@LogObject}";

    private const string DebugMessageTemplate = "{Subject:l}|{LogMessage:l}|{@LogObject}";

    private const string ErrorMessageTemplate = "{LogMessage:l}|{@LogObject}";

    private const string ExceptionTemplate = "{@LogObject}";

    private static ILoggerWrapper logger = new DefaultLoggerWrapper();

    public static void SetLoggerWrapper(ILoggerWrapper wrapper) => logger = wrapper;

    public static void LogInfo(string subject, string message, object? logObject = null)
        => logger.LogInfo(subject, message, logObject);

    public static void LogDebug(string subject, string message, object? logObject = null)
        => logger.LogDebug(subject, message, logObject);

    public static void LogException(string ex, object? logObject = null)
        => logger.LogException(ex, logObject);

    public static void LogException(Exception ex, object? logObject = null)
        => logger.LogException(ex, logObject);

    public class TestLoggerWrapper : ILoggerWrapper
    {
        public void LogInfo(string subject, string message, object? logObject = null) => throw new Exception("Info");

        public void LogDebug(string subject, string message, object? logObject = null) => throw new Exception("Debug");

        public void LogException(string ex, object? logObject = null) => throw new Exception("Exception");

        public void LogException(Exception ex, object? logObject = null) => throw new Exception("Exception");
    }

    private class DefaultLoggerWrapper : ILoggerWrapper
    {
        public void LogInfo(string subject, string message, object? logObject = null) => Log.Information(InfoMessageTemplate, subject, message, logObject);

        public void LogDebug(string subject, string message, object? logObject = null) => Log.Debug(DebugMessageTemplate, subject, message, logObject);

        public void LogException(string ex, object? logObject = null) => Log.Error(ErrorMessageTemplate, ex, logObject);

        public void LogException(Exception ex, object? logObject = null)
        {
            var st = new StackTrace(ex, true);
            var frame = st.GetFrame(0);
            var line = frame.GetFileLineNumber();
            var methodName = frame.GetMethod().ReflectedType.FullName;
            var fileName = frame.GetFileName();

            using (LogContext.PushProperty("file_name", fileName))
            using (LogContext.PushProperty("method_name", methodName))
            using (LogContext.PushProperty("message", ex.Message))
            using (LogContext.PushProperty("line", line))
            {
                Log.Error(ex, ExceptionTemplate, logObject);
            }
        }
    }
}
