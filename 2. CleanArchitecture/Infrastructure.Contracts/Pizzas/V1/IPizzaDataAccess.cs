namespace Infrastructure.Contracts.Pizzas.V1;

using Common.Models.Pizza.V1;

public interface IPizzaDataAccess
{
    Task<Result<PizzaModel>> Get(int id, CancellationToken cancellationToken = default);

    Task<ListResult<PizzaModel>> Search(PizzaSearchModel model, CancellationToken cancellationToken = default);

    Task<Result<PizzaModel>> Save(CreatePizzaModel model, CancellationToken cancellationToken = default);

    Task<Result<PizzaModel>> Update(int id, UpdatePizzaModel model, CancellationToken cancellationToken = default);

    Task<Result> Delete(int id, CancellationToken cancellationToken = default);
}