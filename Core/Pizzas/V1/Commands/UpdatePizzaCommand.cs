namespace Core.Pizzas.V1.Commands;

using Common.Models.Pizza.V1;
using DataAccess.Contracts.Pizzas.V1;

public class UpdatePizzaCommand : IRequest<Result<PizzaModel>>
{
    public int Id { get; set; }

    public UpdatePizzaModel Model { get; set; }

    public class UpdatePizzaCommandHandler : IRequestHandler<UpdatePizzaCommand, Result<PizzaModel>>
    {
        private readonly IPizzaDataAccess dataAccess;

        public UpdatePizzaCommandHandler(IPizzaDataAccess dataAccess)
            => this.dataAccess = dataAccess;

        public async Task<Result<PizzaModel>> Handle(UpdatePizzaCommand request, CancellationToken cancellationToken = default)
            => await this.dataAccess.Update(request.Id, request.Model, cancellationToken);
    }
}