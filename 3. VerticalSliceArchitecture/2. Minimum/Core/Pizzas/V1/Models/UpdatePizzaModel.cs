namespace Core.Pizzas.V1.Models;

public sealed class UpdatePizzaModel
{
    public required string Name { get; set; }

    public bool? Disabled { get; set; }
}
