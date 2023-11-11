namespace Features.Pizzas.V1.Queries;

using Common.Models;
using Features;
using Features.Pizzas.Errors;
using Features.Pizzas.V1.Entities;
using Features.Pizzas.V1.Mappers;
using Features.Pizzas.V1.Models;

public class GetPizzaQuery : IRequest<Result<PizzaModel>>
{
    public int Id { get; set; }
}

public class GetPizzaQueryHandler(DatabaseContext databaseContext) : IRequestHandler<GetPizzaQuery, Result<PizzaModel>>
{
    private static readonly Func<DatabaseContext, int, Task<Pizza>> GetQuery =
        EF.CompileAsyncQuery((DatabaseContext db, int id) => db.Set<Pizza>().FirstOrDefault(c => c.Id == id && c.Disabled == false));

    public async Task<Result<PizzaModel>> Handle(GetPizzaQuery request, CancellationToken cancellationToken = default)
    {
        var result = await GetQuery(databaseContext, request.Id);
        return result == null ? Result<PizzaModel>.NotFound(PizzaErrors.NotFound) : Result<PizzaModel>.Success(result.Map());
    }
}