namespace Core.Pizzas.V1.Endpoints;

public static class DependencyInjectionEndPoints
{
    public static void AddV1Endpoints(this IEndpointRouteBuilder app)
    {
        SearchPizzaEndpoint.MapEndpoints(app);
        CreatePizzaEndpoint.MapEndpoints(app);
        UpdatePizzaEndpoint.MapEndpoints(app);
    }
}
