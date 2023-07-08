namespace Core.Pizzas.V1.Commands;

using Common.Entities.V1;
using Common.Models.Pizza.V1;
using Core.Pizzas.V1.Mappers;

public class CreatePizzaCommand : IRequest<Result<PizzaModel>>
{
    public string Name { get; set; }

    [DefaultValue(false)]
    public bool Disabled { get; set; } = false;
}

public class CreatePizzaCommandHandler(DatabaseContext databaseContext) : IRequestHandler<CreatePizzaCommand, Result<PizzaModel>>
{
    public async Task<Result<PizzaModel>> Handle(CreatePizzaCommand request, CancellationToken cancellationToken = default)
    {
        var entity = new Pizza()
        {
            Name = request?.Name,
            Disabled = request?.Disabled,
            DateCreated = DateTime.UtcNow
        };
        databaseContext.Samples.Add(entity);
        var outcome = await databaseContext.SaveChangesAsync(cancellationToken);

        return ProcessEFResult<PizzaModel>.Outcome(entity.Map(), outcome);
    }
}