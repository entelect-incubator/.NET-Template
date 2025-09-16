namespace Api.Endpoints.V1;

using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Models;

public static class UpdatePizzaEndpoint
{
    public static async Task<IResult> Update(
        UpdatePizza command,
        [FromServices] IUpdatePizzaCommand updatePizzaCommand,
        CancellationToken cancellationToken)
        => ApiMinimalResultHelper.Outcome(await updatePizzaCommand.Handle(command, cancellationToken));

    public static void MapEndpoints(this IEndpointRouteBuilder app)
        => app.MapPut($"{Config.ENDPOINT}", Update)
            .AddEndpointFilter<ValidationFilter<UpdatePizza>>()
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