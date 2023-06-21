namespace GDE.Api.Test;

using System.Collections.Generic;
using Common.Entities.V1;
using Common.Models.Pizza.V1;

public static class PizzaTestData
{
    public static Faker faker = new Faker();

    public static Pizza Pizza = new()
    {
        Id = 1,
        Disabled = false,
        Name = faker.PickRandom(pizzas),
        DateCreated = DateTime.Now
    };

    public static PizzaModel PizzaModel = new()
    {
        Id = 1,
        Disabled = false,
        Name = faker.PickRandom(pizzas),
        DateCreated = DateTime.Now
    };

    public static CreatePizzaModel Create = new()
    {
        Name = faker.PickRandom(pizzas),
        Disabled = false
    };

    public static UpdatePizzaModel Update = new()
    {
        Name = faker.PickRandom(pizzas),
    };

    private static readonly List<string> pizzas = new()
    {
        "Veggie Pizza",
        "Pepperoni Pizza",
        "Meat Pizza",
        "Margherita Pizza",
        "BBQ Chicken Pizza",
        "Hawaiian Pizza"
    };
}
