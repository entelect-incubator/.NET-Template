namespace Core.Pizzas.V1.Commands;

public class CreatePizzaCommandValidator : AbstractValidator<CreatePizza>
{
    public CreatePizzaCommandValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty().NotNull()
            .WithMessage("Pizza name is required");
    }
}
