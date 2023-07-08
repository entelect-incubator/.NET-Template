namespace Core.Pizzas.V1.Commands;

using DataAccess.Contracts.Pizzas.V1;

public class DeletePizzaCommand : IRequest<Result>
{
    public int Id { get; set; }
}

public class DeletePizzaCommandHandler(IPizzaDataAccess dataAccess) : IRequestHandler<DeletePizzaCommand, Result>
{
    public async Task<Result> Handle(DeletePizzaCommand request, CancellationToken cancellationToken = default)
        => await dataAccess.Delete(request.Id, cancellationToken);
}