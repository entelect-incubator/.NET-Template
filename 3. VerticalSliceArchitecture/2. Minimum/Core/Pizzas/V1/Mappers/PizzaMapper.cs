namespace Core.Pizzas.V1.Mappers;

using Core.Pizzas.V1.Database.Entities;
using Core.Pizzas.V1.Models;
using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class PizzaMapper
{
    public static partial PizzaModel Map(this Pizza entity);

    public static partial Pizza Map(this PizzaModel model);

    public static List<PizzaModel> Map(this List<Pizza> entities)
        => entities.Select(x => x.Map()).ToList();
}
