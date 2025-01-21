namespace Core.Pizzas.V1.Entities.V1;

public sealed class Pizza
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public DateTimeOffset? DateCreated { get; set; }

    public bool? Disabled { get; set; }
}
