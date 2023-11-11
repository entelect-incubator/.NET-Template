namespace Core.Pizzas.V1.Commands;

public class UpdatePizzaCommandValidator : AbstractValidator<UpdatePizzaCommand>
{
    public UpdatePizzaCommandValidator()
    {
        this.RuleFor(r => r.Id)
            .NotEmpty();

        this.RuleFor(r => r.Model.Name)
            .MaximumLength(200)
            .NotEmpty();
    }
}
