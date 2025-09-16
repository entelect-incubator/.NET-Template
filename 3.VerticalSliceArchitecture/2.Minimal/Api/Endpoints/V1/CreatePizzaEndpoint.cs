namespace Api.Endpoints.V1;

using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Models;

public static class CreatePizzaEndpoint
{
    public static async Task<IResult> Create(
        CreatePizza command,
        [FromServices] ICreatePizzaCommand createPizzaCommand,
        CancellationToken cancellationToken)
        => ApiMinimalResultHelper.Outcome(await createPizzaCommand.Handle(command, cancellationToken));

    public static void MapEndpoints(this IEndpointRouteBuilder app)
        => app.MapPost($"{Config.ENDPOINT}", Create)
            .AddEndpointFilter<ValidationFilter<CreatePizza>>()
            .WithTags(Config.TAG)
            .WithName("Create a pizza")
            .Produces<Result<PizzaModel>>(StatusCodes.Status200OK, "application/json")
            .WithStandardErrors()
            .WithOpenApi(op =>
            {
                op.OperationId = "CreatePizza";
                op.Summary = "Create a new pizza";
                return op;
            });
}