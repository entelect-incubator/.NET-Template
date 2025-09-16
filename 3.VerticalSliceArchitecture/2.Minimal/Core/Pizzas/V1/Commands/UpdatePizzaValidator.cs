namespace Core.Pizzas.V1.Commands;

public class UpdatePizzaCommandValidator : AbstractValidator<UpdatePizza>
{
    public UpdatePizzaCommandValidator()
    {
        this.RuleFor(x => x.Id).NotEmpty().NotNull()
            .WithMessage("Pizza id is required");
        this.RuleFor(x => x.Model).NotEmpty().NotNull()
            .WithMessage("Pizza update model is required");
    }
}
