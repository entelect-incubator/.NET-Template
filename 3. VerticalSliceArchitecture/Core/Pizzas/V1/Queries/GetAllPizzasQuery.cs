namespace Features.Pizzas.V1.Queries;

using Common.Models;
using Features;
using Features.Pizzas.V1.Filters;
using Features.Pizzas.V1.Mappers;
using Features.Pizzas.V1.Models;
using Microsoft.AspNetCore.Mvc;

[BindProperties]
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