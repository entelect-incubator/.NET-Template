namespace Core.Pizzas.V1.Database.Filters;

using Core.Pizzas.V1.Database.Entities.V1;

public static class PizzaFilter
{
    public static IQueryable<Pizza> FilterByName(this IQueryable<Pizza> query, string name)
        => string.IsNullOrWhiteSpace(name) ? query : query.Where(x => x.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));

    public static IQueryable<Pizza> FilterByPizzaFor(this IQueryable<Pizza> query, bool? disabled)
        => disabled.HasValue || disabled is null ? query : query.Where(x => x.Disabled == disabled.Value);
}
