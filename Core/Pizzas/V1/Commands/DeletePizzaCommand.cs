namespace Core.Pizzas.V1.Commands;

using DataAccess.Contracts.Pizzas.V1;

public class DeletePizzaCommand : IRequest<Result>
{
    public int Id { get; set; }

    public class DeletePizzaCommandHandler : IRequestHandler<DeletePizzaCommand, Result>
    {
        private readonly IPizzaDataAccess dataAccess;

        public DeletePizzaCommandHandler(IPizzaDataAccess dataAccess)
            => this.dataAccess = dataAccess;

        public async Task<Result> Handle(DeletePizzaCommand request, CancellationToken cancellationToken = default)
            => await this.dataAccess.Delete(request.Id, cancellationToken);
    }
}