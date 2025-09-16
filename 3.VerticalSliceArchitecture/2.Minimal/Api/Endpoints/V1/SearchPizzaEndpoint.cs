namespace Api.Endpoints.V1;

using Core.Pizzas.V1.Models;
using Core.Pizzas.V1.Queries;

public static class SearchPizzaEndpoint
{
    public static async Task<IResult> Search(
        GetAllPizzas query,
        [FromServices] IGetAllPizzasQuery getAllPizzasQuery,
        CancellationToken cancellationToken)
        => ApiMinimalResultHelper.Outcome(await getAllPizzasQuery.Handle(query, cancellationToken));

    public static void MapEndpoints(this IEndpointRouteBuilder app)
        => app.MapPost($"{Config.ENDPOINT}search", Search)
            .AddEndpointFilter<ValidationFilter<GetAllPizzas>>()
            .WithTags(Config.TAG)
            .WithName("Search for pizzas")
            .Produces<Result<IEnumerable<PizzaModel>>>(StatusCodes.Status200OK, "application/json")
            .WithStandardErrors()
            .WithOpenApi(op =>
            {
                op.OperationId = "SearchPizzas";
                op.Summary = "Search for pizzas";
                return op;
            });
}