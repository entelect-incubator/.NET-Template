namespace Core.Pizzas.V1.Commands;

using Core.Pizzas.V1.Mappers;
using Core.Pizzas.V1.Models;

public sealed class UpdatePizza
{
    public required int Id { get; set; }

    public required UpdatePizzaModel Model { get; set; }
}

public interface IUpdatePizzaCommand
{
    Task<Result<PizzaModel>> Handle(UpdatePizza request, CancellationToken cancellationToken = default);
}

public class UpdatePizzaCommand(DatabaseContext databaseContext) : IUpdatePizzaCommand
{
    public async Task<Result<PizzaModel>> Handle(UpdatePizza request, CancellationToken cancellationToken = default)
    {
        var model = request.Model;
        var query = EF.CompileAsyncQuery((DatabaseContext db, int id) => db.Pizzas.FirstOrDefault(c => c.Id == id));
        var findEntity = await query(databaseContext, request.Id);
        if (findEntity is null)
        {
            return Result<PizzaModel>.NotFound();
        }

        ActionHelper.UpdateIf(() => findEntity.Name = model.Name, model?.Name);
        ActionHelper.UpdateIf(() => findEntity.Disabled = model.Disabled.Value, model?.Disabled);

        databaseContext.Pizzas.Update(findEntity);
        var outcome = await databaseContext.SaveChangesAsync(cancellationToken);

        return ProcessEFResult<PizzaModel>.Outcome(findEntity.Map(), outcome);
    }
}