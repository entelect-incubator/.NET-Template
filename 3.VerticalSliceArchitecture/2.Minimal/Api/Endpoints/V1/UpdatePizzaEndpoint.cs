namespace Api.Endpoints.V1;

using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Models;

public static class UpdatePizzaEndpoint
{
    public static async Task<IResult> Update(
        UpdatePizzaCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
        => ApiMinimalResultHelper.Outcome(mediator.Send(command, cancellationToken));

    public static void MapEndpoints(this IEndpointRouteBuilder app)
        => app.MapPut($"{Config.ENDPOINT}", Update)
            .WithTags(Config.TAG)
            .WithName("Update pizza")
            .Produces<Result<PizzaModel>>(StatusCodes.Status200OK, "application/json")
            .WithNotFoundAndErrors()
            .WithOpenApi(op =>
            {
                op.OperationId = "UpdatePizza";
                op.Summary = "Update existing pizza";
                return op;
            });
}