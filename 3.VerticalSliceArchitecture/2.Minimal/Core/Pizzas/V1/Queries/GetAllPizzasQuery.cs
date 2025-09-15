namespace Core.Pizzas.V1.Queries;

using Core.Pizzas.V1.Database.Filters;
using Core.Pizzas.V1.Mappers;
using Core.Pizzas.V1.Models;

[BindProperties]
public sealed class GetAllPizzasQuery : BaseSearchModel, IRequest<GetAllPizzasQuery, Result<IEnumerable<PizzaModel>>>
{
    public string? Name { get; set; }
}

public struct GetAllPizzasQueryHandler(DatabaseContext databaseContext) : IRequestHandler<GetAllPizzasQuery, Task<Result<IEnumerable<PizzaModel>>>>
{
    public readonly async Task<Result<IEnumerable<PizzaModel>>> Handle(GetAllPizzasQuery request, CancellationToken cancellationToken = default)
    {
        Logger.LogInfo("Hello", "Test");
        var entities = databaseContext.Pizzas.Select(x => x)
          .AsNoTracking()
          .FilterByName(request?.Name);

        var count = await entities.CountAsync(cancellationToken);
        var paged = await entities.ApplyPaging(request.PagingArgs).ToListAsync(cancellationToken);
        return Result<IEnumerable<PizzaModel>>.Success(paged.Map(), count);
    }
}