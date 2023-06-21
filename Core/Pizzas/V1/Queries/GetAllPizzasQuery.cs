namespace Core.Pizzas.V1.Queries;

using Common.Models.Pizza.V1;
using DataAccess.Contracts.Pizzas.V1;

public class GetAllPizzasQuery : IRequest<ListResult<PizzaModel>>
{
    public PizzaSearchModel Model { get; set; }

    public class GetAllPizzaQueryHandler : IRequestHandler<GetAllPizzasQuery, ListResult<PizzaModel>>
    {
        private readonly IPizzaDataAccess dataAccess;

        public GetAllPizzaQueryHandler(IPizzaDataAccess dataAccess)
            => this.dataAccess = dataAccess;

        public async Task<ListResult<PizzaModel>> Handle(GetAllPizzasQuery request, CancellationToken cancellationToken = default)
            => await this.dataAccess.Search(request.Model, cancellationToken);
    }
}