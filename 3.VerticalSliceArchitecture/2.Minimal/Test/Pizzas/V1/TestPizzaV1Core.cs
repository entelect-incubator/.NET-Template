namespace Test.Pizzas.V1;

using Common;
using Core;
using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Models;
using Core.Pizzas.V1.Queries;
using Test.Setup.TestData.Pizzas.V1;

[TestFixture]
public class TestPizzaV1Core : QueryTestBase
{
    private PizzaModel model;

    private DatabaseContext databaseContext;

    [SetUp]
    public async Task Init()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        configuration.Bind("Settings", new SettingsConfiguration());
        this.databaseContext = Context;
        var sutCreate = new CreatePizzaCommand(this.databaseContext);
        var resultCreate = await sutCreate.Handle(PizzaTestData.Create);

        if (resultCreate.IsError)
        {
            Assert.That(false, Is.False);
        }

        this.model = resultCreate.Data;
    }

    [Test]
    public async Task GetAllAsync()
    {
        var sutGetAll = new GetAllPizzasQuery(this.databaseContext);
        var resultGetAll = await sutGetAll.Handle(new GetAllPizzas { Name = this.model.Name });

        Assert.That(resultGetAll?.Count > 0, Is.True);
    }

    [Test]
    public void SaveAsync()
        => Assert.That(this.model, Is.Not.Null);

    [Test]
    public async Task UpdateAsync()
    {
        var sutUpdate = new UpdatePizzaCommand(this.databaseContext);
        var resultUpdate = await sutUpdate.Handle(
            new UpdatePizza { Id = this.model.Id, Model = new UpdatePizzaModel() { Name = PizzaTestData.Update.Name } });

        Assert.That(resultUpdate.IsError, Is.False);
    }
}