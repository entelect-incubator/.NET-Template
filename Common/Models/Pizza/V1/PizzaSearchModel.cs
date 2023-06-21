namespace Common.Models.Pizza.V1;

using Microsoft.AspNetCore.Mvc;

[BindProperties]
public sealed class PizzaSearchModel : BaseSearchModel
{
    public string? Name { get; set; }

    public DateTimeOffset? DateCreated { get; set; }

    public bool? Disabled { get; set; }
}
