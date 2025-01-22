namespace Features.Pizzas.V1.Commands;

using Common.Models;
using Features;
using Features.Pizzas.Errors;

public class DeletePizzaCommand : IRequest<Result>
{
    public int Id { get; set; }
}

public class DeletePizzaCommandHandler(DatabaseContext databaseContext) : IRequestHandler<DeletePizzaCommand, Result>
{
    public async Task<Result> Handle(DeletePizzaCommand request, CancellationToken cancellationToken = default)
    {
        var entity = await databaseContext.Pizzas.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return Result.NotFound(PizzaErrors.Delete);
        }

        databaseContext.Pizzas.Remove(entity);
        var outcome = await databaseContext.SaveChangesAsync(cancellationToken);

        return ProcessEFResult.Outcome(outcome);
    }
}