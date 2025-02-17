namespace Core.Pizzas.V1.Commands;

using Core.Pizzas.V1.Mappers;
using Core.Pizzas.V1.Models;

public sealed class UpdatePizzaCommand : IRequest<Result<PizzaModel>>
{
    public required int Id { get; set; }

    public required UpdatePizzaModel Model { get; set; }
}

public struct UpdatePizzaCommandHandler(DatabaseContext databaseContext) : IRequestHandler<UpdatePizzaCommand, Result<PizzaModel>>
{
    public readonly async Task<Result<PizzaModel>> Handle(UpdatePizzaCommand request, CancellationToken cancellationToken = default)
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