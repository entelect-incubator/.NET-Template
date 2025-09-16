namespace Test.Setup.TestData.Pizzas.V1;

using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Database.Entities;
using Core.Pizzas.V1.Models;

public static class PizzaTestData
{
    public readonly static Pizza Pizza = new()
    {
        Id = 1,
        Name = "Regina",
        Disabled = false,
        DateCreated = DateTime.Now
    };

    public readonly static PizzaModel PizzaModel = new()
    {
        Id = 1,
        Name = "Hawaiian",
        Disabled = false,
        DateCreated = DateTime.Now
    };

    public readonly static CreatePizza Create = new()
    {
        Name = "Margherita ",
    };

    public readonly static UpdatePizzaModel Update = new()
    {
        Name = "BBQ Chicken Pizza",
    };
}
