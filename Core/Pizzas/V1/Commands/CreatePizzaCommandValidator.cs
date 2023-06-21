namespace Core.Pizzas.V1.Commands;

public class CreatePizzaCommandValidator : AbstractValidator<CreatePizzaCommand>
{
    public CreatePizzaCommandValidator()
    {
        this.RuleFor(r => r.Model.Name)
            .MaximumLength(200)
            .NotEmpty();
    }
}
