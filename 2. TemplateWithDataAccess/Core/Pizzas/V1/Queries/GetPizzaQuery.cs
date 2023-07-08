namespace Core.Pizzas.V1.Queries;

using Common.Models.Pizza.V1;
using DataAccess.Contracts.Pizzas.V1;

public class GetPizzaQuery : IRequest<Result<PizzaModel>>
{
    public int Id { get; set; }
}

public class GetPizzaQueryHandler(IPizzaDataAccess dataAccess) : IRequestHandler<GetPizzaQuery, Result<PizzaModel>>
{
    public async Task<Result<PizzaModel>> Handle(GetPizzaQuery request, CancellationToken cancellationToken = default)
        => await dataAccess.Get(request.Id, cancellationToken);
}