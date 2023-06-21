namespace Core.Pizzas.V1.Commands;

using Common.Models.Pizza.V1;
using DataAccess.Contracts.Pizzas.V1;

public class CreatePizzaCommand : IRequest<Result<PizzaModel>>
{
    public CreatePizzaModel Model { get; set; }
}

public class CreatePizzaCommandHandler : IRequestHandler<CreatePizzaCommand, Result<PizzaModel>>
{
    private readonly IPizzaDataAccess dataAccess;

    public CreatePizzaCommandHandler(IPizzaDataAccess dataAccess)
        => this.dataAccess = dataAccess;

    public async Task<Result<PizzaModel>> Handle(CreatePizzaCommand request, CancellationToken cancellationToken = default)
        => await this.dataAccess.Save(request.Model, cancellationToken);
}