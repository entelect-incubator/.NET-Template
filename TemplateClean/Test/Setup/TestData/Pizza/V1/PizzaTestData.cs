namespace Test.Setup.TestData.Pizza.V1;

using System.Collections.Generic;
using Common.Entities.V1;
using Common.Models.Pizza.V1;
using Core.Pizzas.V1.Commands;

public static class PizzaTestData
{
    public static Pizza Pizza = new()
    {
        Id = 1,
        Disabled = false,
        Name = "Pepperoni Pizza",
        DateCreated = DateTime.Now
    };

    public static PizzaModel PizzaModel = new()
    {
        Id = 1,
        Disabled = false,
        Name = "Hawaiian Pizza",
        DateCreated = DateTime.Now
    };

    public static CreatePizzaCommand Create = new()
    {
        Name = "Margherita Pizza",
        Disabled = false
    };

    public static UpdatePizzaModel Update = new()
    {
        Name = "BBQ Chicken Pizza",
    };
}
