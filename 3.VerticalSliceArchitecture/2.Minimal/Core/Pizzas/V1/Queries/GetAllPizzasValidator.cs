namespace Core.Pizzas.V1.Queries;

public class GetAllPizzasValidator : AbstractValidator<GetAllPizzas>
{
    public GetAllPizzasValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().When(x => x.Name != null)
            .WithMessage("Pizza name cannot be empty if provided");

        // OrderBy is optional, but if present, must not be empty
        RuleFor(x => x.OrderBy)
            .NotEmpty().When(x => !string.IsNullOrWhiteSpace(x.OrderBy))
            .WithMessage("OrderBy cannot be empty if provided");

        RuleFor(x => x.SortDirection)
            .IsInEnum();

        RuleFor(x => x.PagingArgs)
            .SetValidator(new PagingArgsValidator()).When(x => x.PagingArgs != null);
    }
}

public class PagingArgsValidator : AbstractValidator<PagingArgs>
{
    public PagingArgsValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page Number must be at least 1");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page Size must be at least 1");
    }
}
