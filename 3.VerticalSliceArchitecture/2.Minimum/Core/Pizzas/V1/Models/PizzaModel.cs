namespace Core.Pizzas.V1.Models;

public sealed class PizzaModel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public DateTimeOffset? DateCreated { get; set; }

    public bool? Disabled { get; set; }
}
