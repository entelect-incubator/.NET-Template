namespace Core.Pizzas.V1.Queries;

using Common.Models.Pizza.V1;
using DataAccess.Contracts.Pizzas.V1;

public class GetAllPizzasQuery : IRequest<ListResult<PizzaModel>>
{
    public PizzaSearchModel Model { get; set; }

    public class GetAllPizzaQueryHandler(IPizzaDataAccess dataAccess) : IRequestHandler<GetAllPizzasQuery, ListResult<PizzaModel>>
    {
        public async Task<ListResult<PizzaModel>> Handle(GetAllPizzasQuery request, CancellationToken cancellationToken = default)
            => await dataAccess.Search(request.Model, cancellationToken);
    }
}