namespace Test.Pizzas.V1;

using Common.Models.Pizza.V1;
using DataAccess.Contracts.Pizzas.V1;
using DataAccess.Pizzas.V1;
using Test.Setup.TestData.Pizza;

[TestFixture]
public class TestPizzaeV1DataAccess : QueryTestBase
{
    private IPizzaDataAccess handler;

    private PizzaModel model;

    [SetUp]
    public async Task Init()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        configuration.Bind("Settings", new Settings());

        this.handler = new PizzaDataAccess(this.Context);
        var result = await this.handler.Save(PizzaTestData.Create);
        if (result.IsError)
        {
            Assert.That(false, Is.False);
        }

        this.model = result.Data;
    }

    [Test]
    public async Task GetAsync()
    {
        var response = await this.handler.Get(this.model.Id);
        Assert.That(response, Is.Not.Null);
    }

    [Test]
    public async Task GetAllAsync()
    {
        var response = await this.handler.Search(new PizzaSearchModel
        {
            Name = this.model.Name
        });
        var outcome = response.Count;

        Assert.That(outcome == 1, Is.True);
    }

    [Test]
    public async Task UpdateAsync()
    {
        this.model.Name = "Test";
        var response = await this.handler.Update(this.model.Id, PizzaTestData.Update);
        Assert.That(response != null && !response.IsError, Is.True);

        var outcome = response.Data.Name.Equals(PizzaTestData.Update.Name);
        Assert.That(outcome, Is.True);
    }

    [Test]
    public async Task DeleteAsync()
    {
        var response = await this.handler.Delete(this.model.Id);
        Assert.That(response != null && !response.IsError, Is.True);
    }
}