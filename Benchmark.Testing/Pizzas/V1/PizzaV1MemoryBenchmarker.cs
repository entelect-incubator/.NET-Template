namespace Benchmark.Testing.Samples.V1;

using Common.Models.Pizza.V1;
using DataAccess.Contracts.Pizzas.V1;
using DataAccess.Pizzas.V1;
using Test.Setup;
using Test.Setup.TestData.Pizza;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[HtmlExporter]
[RPlotExporter]
public class PizzaV1MemoryBenchmarker : QueryTestBase
{
    private IPizzaDataAccess? handler;

    [GlobalSetup]
    public void Setup()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        configuration.Bind("Settings", new Settings());
    }

    [Benchmark]
    public async Task TestCreate()
    {
        this.handler = new PizzaDataAccess(this.Context);

        var saveResult = await this.handler.Save(PizzaTestData.Create);
        if (!saveResult.IsError)
        {
            var getResult = await this.handler.Get(saveResult.Data.Id);
        }
    }

    [Benchmark]
    public async Task TestGet()
    {
        this.handler = new PizzaDataAccess(this.Context);

        var result = await this.handler.Save(PizzaTestData.Create);
        if (!result.IsError)
        {
            var model = await this.handler.Get(result.Data.Id);
        }
    }

    [Benchmark]
    public async Task TestUpdate()
    {
        this.handler = new PizzaDataAccess(this.Context);

        var result = await this.handler.Save(PizzaTestData.Create);
        if (!result.IsError)
        {
            var getResult = await this.handler.Get(result.Data.Id);
            if (!getResult.IsError)
            {
                var updateResult = await this.handler.Update(getResult.Data.Id, new UpdatePizzaModel
                {
                    Name = "New name"
                });
            }
        }
    }

    [Benchmark]
    public async Task TestDelete()
    {
        this.handler = new PizzaDataAccess(this.Context);

        var result = await this.handler.Save(PizzaTestData.Create);
        if (!result.IsError)
        {
            var getResult = await this.handler.Get(result.Data.Id);
            if (!getResult.IsError)
            {
                var deleteResult = await this.handler.Delete(getResult.Data.Id);
            }
        }
    }
}
