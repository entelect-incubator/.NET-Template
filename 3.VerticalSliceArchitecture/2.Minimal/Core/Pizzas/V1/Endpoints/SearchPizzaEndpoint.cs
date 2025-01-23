namespace Core.Pizzas.V1.Endpoints;

using Core.Pizzas.V1.Models;
using Core.Pizzas.V1.Queries;

public static class SearchPizzaEndpoint
{
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<IEnumerable<PizzaModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
    [OpenApiOperation("Search", "Search for pizzas")]
    public static async Task<IResult> Search([FromBody] GetAllPizzasQuery query, IMediator mediator, CancellationToken cancellationToken)
        => ApiMinimalResultHelper.Outcome(await mediator.Send(query, cancellationToken));

    public static void MapEndpoints(IEndpointRouteBuilder app)
        => app.MapPost($"{Config.ENDPOINT}search", Search).WithTags(Config.TAG).WithName("Search for pizzas");
}