namespace Test.Pizzas.V1;

using Common.Models.Pizza.V1;
using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Queries;
using DataAccess.Contracts.Pizzas.V1;
using DataAccess.Pizzas.V1;
using Test.Setup.TestData.Pizza;
using static Core.Pizzas.V1.Commands.DeletePizzaCommand;
using static Core.Pizzas.V1.Commands.UpdatePizzaCommand;
using static Core.Pizzas.V1.Queries.GetAllPizzasQuery;
using static Core.Pizzas.V1.Queries.GetPizzaQuery;

[TestFixture]
public class TestPizzaV1Core : QueryTestBase
{
    private IPizzaDataAccess dataAccess;

    private PizzaModel model;

    [SetUp]
    public async Task Init()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        configuration.Bind("Settings", new Settings());

        this.dataAccess = new PizzaDataAccess(this.Context);
        var sutCreate = new CreatePizzaCommandHandler(this.dataAccess);
        var resultCreate = await sutCreate.Handle(
            new CreatePizzaCommand
            {
                Model = PizzaTestData.Create
            }, CancellationToken.None);

        if (resultCreate.IsError)
        {
            Assert.IsTrue(false);
        }

        this.model = resultCreate.Data;
    }

    [Test]
    public async Task GetAsync()
    {
        var sutGet = new GetPizzaQueryHandler(this.dataAccess);
        var resultGet = await sutGet.Handle(
            new GetPizzaQuery
            {
                Id = this.model.Id
            }, CancellationToken.None);

        Assert.IsTrue(resultGet?.Data != null);
    }

    [Test]
    public async Task GetAllAsync()
    {
        var sutGetAll = new GetAllPizzaQueryHandler(this.dataAccess);
        var resultGetAll = await sutGetAll.Handle(
            new GetAllPizzasQuery
            {
                Model = new PizzaSearchModel
                {
                    Name = this.model.Name
                }
            }, CancellationToken.None);

        Assert.IsTrue(resultGetAll?.Data.Count > 0);
    }

    [Test]
    public void SaveAsync() => Assert.IsTrue(this.model != null);

    [Test]
    public async Task UpdateAsync()
    {
        var sutUpdate = new UpdatePizzaCommandHandler(this.dataAccess);
        var resultUpdate = await sutUpdate.Handle(
            new UpdatePizzaCommand
            {
                Id = this.model.Id,
                Model = new UpdatePizzaModel
                {
                    Name = "Test"
                }
            }, CancellationToken.None);

        Assert.IsTrue(!resultUpdate.IsError);
    }

    [Test]
    public async Task DeleteAsync()
    {
        var sutDelete = new DeletePizzaCommandHandler(this.dataAccess);
        var outcomeDelete = await sutDelete.Handle(
            new DeletePizzaCommand
            {
                Id = this.model.Id
            }, CancellationToken.None);

        Assert.IsTrue(!outcomeDelete.IsError);
    }
}