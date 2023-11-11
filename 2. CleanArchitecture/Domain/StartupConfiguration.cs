namespace Domain;

[ExcludeFromCodeCoverage]
public sealed class StartupConfiguration
{
    public string SwaggerDocTitle { get; set; } = "API";

    public string DisplayName { get; set; } = "Pezza";

    public string SwaggerDocVersion { get; set; } = "v1";

    public bool IncludeHeaderVersion { get; set; } = true;

    public bool IncludeCorrelationId { get; set; } = true;

    public bool IncludeSwaggerDoc { get; set; } = true;

    public bool IncludeAuthorization { get; set; } = true;

    public bool UseSwaggerAuth { get; set; } = false;

    public string SwaggerUsername { get; set; } = string.Empty;

    public string SwaggerPassword { get; set; } = string.Empty;
}
