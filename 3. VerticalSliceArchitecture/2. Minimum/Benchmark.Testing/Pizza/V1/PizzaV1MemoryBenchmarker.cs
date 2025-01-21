namespace Benchmark.Testing.Pizza.V1;

using Core.Pizzas.V1.Commands;
using Core.Pizzas.V1.Queries;
using Test.Setup;
using Test.Setup.TestData.Pizzas.V1;
using Utilities;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[HtmlExporter]
[RPlotExporter]
public class PizzaV1MemoryBenchmarker : QueryTestBase
{
    [GlobalSetup]
    public static void Setup()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        configuration.Bind("Settings", new SettingsConfiguration());
    }

    [Benchmark]
    public async Task TestCast()
    {
        var sutCast = new CreatePizzaCommandHandler(this.Context);
        var resultCast = await sutCast.Handle(new CreatePizzaCommand()
        {
            Name = PizzaTestData.Create.Name
        }, CancellationToken.None);

        if (!resultCast.IsError)
        {
            var sutGet = new GetAllPizzasQueryHandler(this.Context);
        }
    }
}
