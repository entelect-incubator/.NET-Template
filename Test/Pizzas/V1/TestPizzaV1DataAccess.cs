namespace Test.Samples.DataAccess;

using Common.Models.Pizza.V1;
using global::DataAccess.Contracts.Pizzas.V1;
using global::DataAccess.Pizzas.V1;

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
            Assert.IsTrue(false);
        }

        this.model = result.Data;
    }

    [Test]
    public async Task GetAsync()
    {
        var response = await this.handler.Get(this.model.Id);
        Assert.IsTrue(response != null);
    }

    [Test]
    public async Task GetAllAsync()
    {
        var response = await this.handler.Search(new PizzaSearchModel
        {
            Name = this.model.Name
        });
        var outcome = response.Count;

        Assert.IsTrue(outcome == 1);
    }

    [Test]
    public async Task UpdateAsync()
    {
        var originalModel = this.model;
        this.model.Name = "Test";
        var response = await this.handler.Update(this.model.Id, PizzaTestData.Update);
        Assert.IsTrue(response != null && !response.IsError);

        var outcome = response.Data.Name.Equals(PizzaTestData.Update.Name);
        Assert.IsTrue(outcome);
    }

    [Test]
    public async Task DeleteAsync()
    {
        var response = await this.handler.Delete(this.model.Id);
        Assert.IsTrue(response != null && !response.IsError);
    }
}