namespace Test.Setup.TestData.Pizzas.V1;

using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Entities.V1;
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

    public readonly static CreatePizzaCommand Create = new()
    {
        Name = "Margherita ",
    };
}
