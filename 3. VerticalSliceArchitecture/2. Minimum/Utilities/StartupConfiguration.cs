namespace Utilities;
[ExcludeFromCodeCoverage]
public sealed class StartupConfiguration
{
    public OpenApi OpenApi { get; set; }

    public string DisplayName { get; set; } = "Vertical Slice Minimum Api";

    public bool IncludeHeaderVersion { get; set; } = true;

    public bool IncludeCorrelationId { get; set; } = true;

    public bool IncludeAuthorization { get; set; } = true;
}

[ExcludeFromCodeCoverage]
public sealed class OpenApi
{
    public string Title { get; set; } = "API";

    public string Version { get; set; } = "v1";

    public bool Enable { get; set; } = true;

    public bool UseAuth { get; set; } = false;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}