namespace Features.Pizzas.V1.Commands;
public class DeletePizzaCommandValidator : AbstractValidator<DeletePizzaCommand>
{
    public DeletePizzaCommandValidator()
    {
        this.RuleFor(r => r.Id)
            .NotEmpty();
    }
}
