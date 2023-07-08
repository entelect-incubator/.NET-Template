namespace DataAccess.Pizzas.V1;

using Common.Entities.V1;
using Common.Extensions;
using Common.Models.Pizza.V1;
using DataAccess.Contracts.Pizzas.V1;

public class PizzaDataAccess : BaseDataAccess<DatabaseContext>, IPizzaDataAccess
{
    private static readonly Func<DatabaseContext, int, Task<Pizza>> GetQuery =
        EF.CompileAsyncQuery((DatabaseContext db, int id) => db.Set<Pizza>().FirstOrDefault(c => c.Id == id && c.Disabled == false));

    public PizzaDataAccess(DatabaseContext databaseContext)
        : base(databaseContext)
    {
    }

    public async Task<Result<PizzaModel>> Get(int id, CancellationToken cancellationToken = default)
    {
        var result = await GetQuery(this.DatabaseContext, id);
        return result == null ? Result<PizzaModel>.NotFound(PizzaErrors.NotFound) : Result<PizzaModel>.Success(result.Map());
    }

    public async Task<ListResult<PizzaModel>> Search(PizzaSearchModel model, CancellationToken cancellationToken = default)
    {
        var entities = this.DatabaseContext.Samples.Select(x => x)
          .AsNoTracking()
          .FilterByName(model?.Name)
          .FilterByDisabled(model?.Disabled);

        var count = entities.Count();
        var paged = await entities.ApplyPaging(model.PagingArgs).ToListAsync(cancellationToken);
        return ListResult<PizzaModel>.Success(paged.Map(), count);
    }

    public async Task<Result<PizzaModel>> Save(CreatePizzaModel model, CancellationToken cancellationToken = default)
    {
        var entity = new Pizza()
        {
            Name = model?.Name,
            Disabled = model?.Disabled,
            DateCreated = DateTime.UtcNow
        };
        this.DatabaseContext.Samples.Add(entity);
        var outcome = await this.DatabaseContext.SaveChangesAsync(cancellationToken);

        return ProcessEFResult<PizzaModel>.Outcome(entity.Map(), outcome);
    }

    public async Task<Result<PizzaModel>> Update(int id, UpdatePizzaModel model, CancellationToken cancellationToken = default)
    {
        var findEntity = await this.DatabaseContext.Samples.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (findEntity == null)
        {
            return Result<PizzaModel>.NotFound(PizzaErrors.Update);
        }

        if (!string.IsNullOrEmpty(model.Name))
        {
            findEntity.Name = model.Name;
        }

        if (model.Disabled.HasValue)
        {
            findEntity.Disabled = model.Disabled.Value;
        }

        this.DatabaseContext.Samples.Update(findEntity);
        var outcome = await this.DatabaseContext.SaveChangesAsync(cancellationToken);

        return ProcessEFResult<PizzaModel>.Outcome(findEntity.Map(), outcome);
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken = default)
    {
        var entity = await this.DatabaseContext.Samples.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity == null)
        {
            return Result.NotFound(PizzaErrors.Delete);
        }

        this.DatabaseContext.Samples.Remove(entity);
        var outcome = await this.DatabaseContext.SaveChangesAsync(cancellationToken);

        return ProcessEFResult.Outcome(outcome);
    }
}
