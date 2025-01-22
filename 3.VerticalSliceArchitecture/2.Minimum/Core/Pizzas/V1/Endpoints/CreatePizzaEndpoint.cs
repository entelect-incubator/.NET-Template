namespace Core.Pizzas.V1.Endpoints;

using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Models;

public static class CreatePizzaEndpoint
{
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<PizzaModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
    [OpenApiOperation("Create", "Create a new pizza")]
    public static async Task<IResult> Create([FromBody] CreatePizzaCommand command, IMediator mediator, CancellationToken cancellationToken)
        => ApiMinimumResultHelper.Outcome(await mediator.Send(command, cancellationToken));

    public static void MapEndpoints(this IEndpointRouteBuilder app)
        => app.MapPost($"{Config.ENDPOINT}", Create).WithTags(Config.TAG).WithName("Create a pizza");
}