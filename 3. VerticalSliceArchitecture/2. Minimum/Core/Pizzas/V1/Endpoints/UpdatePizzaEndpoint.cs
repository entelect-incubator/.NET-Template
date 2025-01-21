namespace Core.Pizzas.V1.Endpoints;

using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Models;

public static class UpdatePizzaEndpoint
{
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<PizzaModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
    [OpenApiOperation("Update", "Update existing pizza")]
    public static async Task<IResult> Update([FromBody] UpdatePizzaCommand command, IMediator mediator, CancellationToken cancellationToken)
        => ApiMinimumResultHelper.Outcome(await mediator.Send(command, cancellationToken));

    public static void MapEndpoints(IEndpointRouteBuilder app)
        => app.MapPut($"{Config.ENDPOINT}", Update).WithTags(Config.TAG).WithName("Update pizza");
}