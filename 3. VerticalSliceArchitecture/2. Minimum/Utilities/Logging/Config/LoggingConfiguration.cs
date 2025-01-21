namespace Utilities.Logging.Config;

[ExcludeFromCodeCoverage]
public sealed class LoggingConfiguration
{
    public string SolutionName { get; set; }

    public string Environment { get; set; } = "env";

    public bool IncludeDebugLogs { get; set; }

    public bool IncludeWriteToFile { get; set; }

    public bool IncludeSystemInfoLogs { get; set; } = false;

    public string FilePath { get; set; }
}
