namespace Application.Pizzas.V1.Filters;

using Domain.Entities.V1;

public static class PizzaFilter
{
    public static IQueryable<Pizza> FilterByDisabled(this IQueryable<Pizza> query, bool? disabled)
    {
        if (!disabled.HasValue)
        {
            return query;
        }

        return query.Where(x => x.Disabled == disabled.Value);
    }

    public static IQueryable<Pizza> FilterByName(this IQueryable<Pizza> query, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return query;
        }

        return query.Where(x => x.Name.Contains(name));
    }
}
