namespace Application.Pizzas.V1.Queries;

using Application.Pizzas.V1.Filters;
using Domain.Models.Pizza.V1;
using Application.Pizzas.V1.Mappers;
using Domain.Extensions;

public class GetAllPizzasQuery : BaseSearchModel, IRequest<ListResult<PizzaModel>>
{
    public string? Name { get; set; }

    public DateTimeOffset? DateCreated { get; set; }

    public bool? Disabled { get; set; }
}

public class GetAllPizzaQueryHandler(DatabaseContext databaseContext) : IRequestHandler<GetAllPizzasQuery, ListResult<PizzaModel>>
{
    public async Task<ListResult<PizzaModel>> Handle(GetAllPizzasQuery request, CancellationToken cancellationToken = default)
    {
        var entities = databaseContext.Pizzas.Select(x => x)
          .AsNoTracking()
          .FilterByName(request?.Name)
          .FilterByDisabled(request?.Disabled);

        var count = entities.Count();
        var paged = await entities.ApplyPaging(request.PagingArgs).ToListAsync(cancellationToken);
        return ListResult<PizzaModel>.Success(paged.Map(), count);
    }
}