namespace Common.Models.Pizza.V1;

using Common.Models;
using Microsoft.AspNetCore.Mvc;

[BindProperties]
public sealed class PizzaSearchModel : BaseSearchModel
{
    public string? Name { get; set; }

    public DateTime? DateCreated { get; set; }

    public bool? Disabled { get; set; }
}
