namespace Core.Pizzas.V1.Commands;

using Core.Pizzas.V1.Database.Entities;
using Core.Pizzas.V1.Mappers;
using Core.Pizzas.V1.Models;

public sealed class CreatePizza
{
    public required string Name { get; set; }
}

public interface ICreatePizzaCommand
{
    Task<Result<PizzaModel>> Handle(CreatePizza request, CancellationToken cancellationToken = default);
}

public class CreatePizzaCommand(DatabaseContext databaseContext) : ICreatePizzaCommand
{
    public async Task<Result<PizzaModel>> Handle(CreatePizza request, CancellationToken cancellationToken = default)
    {
        var entity = new Pizza()
        {
            Name = request.Name,
            Disabled = false,
            DateCreated = DateTime.UtcNow
        };

        databaseContext.Pizzas.Add(entity);
        var outcome = await databaseContext.SaveChangesAsync(cancellationToken);

        return ProcessEFResult<PizzaModel>.Outcome(entity.Map(), outcome);
    }
}