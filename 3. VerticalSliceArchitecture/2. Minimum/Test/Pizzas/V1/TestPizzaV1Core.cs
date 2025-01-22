namespace Test.Pizzas.V1;

using Core;
using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Models;
using Core.Pizzas.V1.Queries;
using Test.Setup.TestData.Pizzas.V1;
using Common;

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
        this.databaseContext = this.Context;
        var sutCreate = new CreatePizzaCommandHandler(this.databaseContext);
        var resultCreate = await sutCreate.Handle(PizzaTestData.Create, CancellationToken.None);

        if (resultCreate.IsError)
        {
            Assert.That(false, Is.False);
        }

        this.model = resultCreate.Data;
    }

    [Test]
    public async Task GetAllAsync()
    {
        var sutGetAll = new GetAllPizzasQueryHandler(this.databaseContext);
        var resultGetAll = await sutGetAll.Handle(new GetAllPizzasQuery { Name = this.model.Name }, CancellationToken.None);

        Assert.That(resultGetAll?.Count > 0, Is.True);
    }

    [Test]
    public void SaveAsync()
        => Assert.That(this.model, Is.Not.Null);
}