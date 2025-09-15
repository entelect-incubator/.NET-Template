namespace Api.Endpoints;

public static class EndpointResponseExtensions
{
    /// <summary>
    /// Adds standard error responses (400 + 500) to the endpoint.
    /// </summary>
    public static RouteHandlerBuilder WithStandardErrors(this RouteHandlerBuilder builder)
        => builder
            .Produces<ValidationErrorResult>(StatusCodes.Status400BadRequest, "application/json")
            .Produces<ErrorResult>(StatusCodes.Status500InternalServerError, "application/json");

    /// <summary>
    /// Adds 404 + standard errors (400 + 500).
    /// </summary>
    public static RouteHandlerBuilder WithNotFoundAndErrors(this RouteHandlerBuilder builder)
        => builder
            .Produces<NotFoundErrorResult>(StatusCodes.Status404NotFound, "application/json")
            .WithStandardErrors();
}
