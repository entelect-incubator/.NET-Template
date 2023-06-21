namespace Api.Controllers.V1;

using Common.Models.Pizza.V1;
using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Queries;

public class PizzaController : ApiController
{
    /// <summary>
    ///     Get Pizza by Id.
    /// </summary>
    /// <param name="id">int.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Result<PizzaModel>>> Get(int id, CancellationToken cancellationToken = default)
    {
        var result = await this.Mediator.Send(new GetPizzaQuery { Id = id }, cancellationToken);
        return ApiResponseHelper.ResponseOutcome(result, this);
    }

    /// <summary>
    ///     Get all Pizzas.
    /// </summary>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    /// <param name="model">Pizza Search Model</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    [HttpPost]
    [Route("Search")]
    public async Task<ActionResult<ListResult<PizzaModel>>> Search(PizzaSearchModel model, CancellationToken cancellationToken = default)
    {
        var result = await this.Mediator.Send(
            new GetAllPizzasQuery
            {
                Model = model
            }, cancellationToken);

        return ApiResponseHelper.ResponseOutcome(result, this);
    }

    /// <summary>
    ///     Create Pizza.
    /// </summary>
    /// <remarks>
    ///     Pizza request:
    ///     POST Pizza
    ///     {
    ///         "name" : "sample"
    ///     }
    /// </remarks>
    /// <param name="model">Pizza Create Model.</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    [HttpPost]
    public async Task<ActionResult<Result<PizzaModel>>> Create(CreatePizzaModel model, CancellationToken cancellationToken = default)
    {
        var result = await this.Mediator.Send(
            new CreatePizzaCommand
            {
                Model = model
            }, cancellationToken);

        return ApiResponseHelper.ResponseOutcome(result, this);
    }

    /// <summary>
    ///     Update Pizza.
    /// </summary>
    /// <remarks>
    ///     Pizza request:
    ///     PATCH Pizza/9a3117a7-2d1f-4a2d-8fac-7f022539ea50
    ///     {
    ///         "name" : "sample 2"
    ///     }
    /// </remarks>
    /// <param name="id">Id.</param>
    /// <param name="model">Pizza Update Model.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    [HttpPatch("{id}")]
    public async Task<ActionResult<Result<PizzaModel>>> Update(int id, UpdatePizzaModel model, CancellationToken cancellationToken = default)
    {
        var result = await this.Mediator.Send(
            new UpdatePizzaCommand
            {
                Id = id,
                Model = model,
            }, cancellationToken);

        return ApiResponseHelper.ResponseOutcome(result, this);
    }

    /// <summary>
    ///     Remove Pizza by Id.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(int id, CancellationToken cancellationToken = default)
    {
        var result = await this.Mediator.Send(new DeletePizzaCommand { Id = id }, cancellationToken);

        return ApiResponseHelper.ResponseOutcome(result, this);
    }
}
