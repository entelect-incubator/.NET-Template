namespace Common.Models.Pizza.V1;

public sealed class PizzaModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime? DateCreated { get; set; }

    public bool? Disabled { get; set; }
}
