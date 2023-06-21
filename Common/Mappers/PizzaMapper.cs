namespace Common.Mappers;

using Common.Entities.V1;
using Common.Models.Pizza.V1;

public static partial class SampleMapper
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

    public static List<PizzaModel> Map(this List<Pizza> pizzas)
        => pizzas.Select(x => x.Map()).ToList();
}
