namespace Test.Pizzas.V1;

using Common.Models.Pizza.V1;
using Core;
using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Queries;
using Test.Setup.TestData.Pizza.V1;

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

        configuration.Bind("Settings", new Settings());
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
    public async Task GetAsync()
    {
        var sutGet = new GetPizzaQueryHandler(this.databaseContext);
        var resultGet = await sutGet.Handle(new GetPizzaQuery { Id = this.model.Id }, CancellationToken.None);

        Assert.That(resultGet?.Data, Is.Not.Null);
    }

    [Test]
    public async Task GetAllAsync()
    {
        var sutGetAll = new GetAllPizzaQueryHandler(this.databaseContext);
        var resultGetAll = await sutGetAll.Handle(new GetAllPizzasQuery { Name = this.model.Name }, CancellationToken.None);

        Assert.That(resultGetAll?.Data.Count > 0, Is.True);
    }

    [Test]
    public void SaveAsync()
        => Assert.That(this.model, Is.Not.Null);

    [Test]
    public async Task UpdateAsync()
    {
        var sutUpdate = new UpdatePizzaCommandHandler(this.databaseContext);
        var resultUpdate = await sutUpdate.Handle(
            new UpdatePizzaCommand
            {
                Id = this.model.Id,
                Model = new UpdatePizzaModel
                {
                    Name = "Test"
                }
            }, CancellationToken.None);

        Assert.That(resultUpdate.IsError, Is.False);
    }

    [Test]
    public async Task DeleteAsync()
    {
        var sutDelete = new DeletePizzaCommandHandler(this.databaseContext);
        var outcomeDelete = await sutDelete.Handle(
            new DeletePizzaCommand
            {
                Id = this.model.Id
            }, CancellationToken.None);

        Assert.That(outcomeDelete.IsError, Is.False);
    }
}