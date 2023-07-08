namespace Core.Pizzas.V1.Commands;

using Common.Models.Pizza.V1;
using DataAccess.Contracts.Pizzas.V1;

public class UpdatePizzaCommand : IRequest<Result<PizzaModel>>
{
    public int Id { get; set; }

    public UpdatePizzaModel Model { get; set; }
}

public class UpdatePizzaCommandHandler(IPizzaDataAccess dataAccess) : IRequestHandler<UpdatePizzaCommand, Result<PizzaModel>>
{
    public async Task<Result<PizzaModel>> Handle(UpdatePizzaCommand request, CancellationToken cancellationToken = default)
        => await dataAccess.Update(request.Id, request.Model, cancellationToken);
}