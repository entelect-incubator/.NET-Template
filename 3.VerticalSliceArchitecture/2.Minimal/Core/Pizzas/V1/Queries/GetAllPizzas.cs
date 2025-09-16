namespace Core.Pizzas.V1.Queries;

using Core.Pizzas.V1.Database.Filters;
using Core.Pizzas.V1.Mappers;
using Core.Pizzas.V1.Models;

[BindProperties]
public sealed class GetAllPizzas : BaseSearchModel
{
    public string? Name { get; set; }
}

public interface IGetAllPizzasQuery
{
    Task<Result<IEnumerable<PizzaModel>>> Handle(GetAllPizzas request, CancellationToken cancellationToken = default);
}

public class GetAllPizzasQuery(DatabaseContext databaseContext) : IGetAllPizzasQuery
{
    public async Task<Result<IEnumerable<PizzaModel>>> Handle(GetAllPizzas request, CancellationToken cancellationToken = default)
    {
        var entities = databaseContext.Pizzas.Select(x => x)
          .AsNoTracking()
          .FilterByName(request?.Name);

        var count = await entities.CountAsync(cancellationToken);
        var paged = await entities.ApplyPaging(request.PagingArgs).ToListAsync(cancellationToken);
        return Result<IEnumerable<PizzaModel>>.Success(paged.Map(), count);
    }
}