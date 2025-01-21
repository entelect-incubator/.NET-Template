namespace Core.Pizzas.V1.Mappers;

using Core.Pizzas.V1.Entities.V1;
using Core.Pizzas.V1.Models;

public static partial class PizzaMapper
{
    public static PizzaModel Map(this Pizza entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            DateCreated = entity.DateCreated,
            Disabled = entity.Disabled
        };

    public static Pizza Map(this PizzaModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            DateCreated = model.DateCreated,
            Disabled = model.Disabled
        };

    public static List<PizzaModel> Map(this List<Pizza> entities)
        => entities.Select(x => x.Map()).ToList();
}
