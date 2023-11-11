namespace Features.Pizzas.V1.Commands;

using Common.Models;
using Features;
using Features.Pizzas.V1.Entities;
using Features.Pizzas.V1.Mappers;
using Features.Pizzas.V1.Models;

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
        databaseContext.Pizzas.Add(entity);
        var outcome = await databaseContext.SaveChangesAsync(cancellationToken);

        return ProcessEFResult<PizzaModel>.Outcome(entity.Map(), outcome);
    }
}