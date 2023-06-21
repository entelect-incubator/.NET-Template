namespace Common.Models.Pizza.V1;

using System.ComponentModel;

public sealed class CreatePizzaModel
{
    public string Name { get; set; }

    [DefaultValue(false)]
    public bool Disabled { get; set; } = false;
}
