﻿namespace Benchmark.Testing.Samples.V1;

using Domain.Models.Pizza.V1;
using Application.Pizzas.V1.Commands;
using Application.Pizzas.V1.Queries;
using Test.Setup;
using Test.Setup.TestData.Pizza.V1;
using Domain;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[HtmlExporter]
[RPlotExporter]
public class PizzaV1MemoryBenchmarker : QueryTestBase
{
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
        var sutCreate = new CreatePizzaCommandHandler(Context);
        var resultCreate = await sutCreate.Handle(PizzaTestData.Create, CancellationToken.None);

        if (!resultCreate.IsError)
        {
            var sutGet = new GetPizzaQueryHandler(Context);
            var resultGet = await sutGet.Handle(new GetPizzaQuery { Id = resultCreate.Data.Id }, CancellationToken.None);
        }
    }

    [Benchmark]
    public async Task TestGet()
    {
        var sutCreate = new CreatePizzaCommandHandler(Context);
        var resultCreate = await sutCreate.Handle(PizzaTestData.Create, CancellationToken.None);

        if (!resultCreate.IsError)
        {
            var sutGet = new GetPizzaQueryHandler(Context);
            var resultGet = await sutGet.Handle(new GetPizzaQuery { Id = resultCreate.Data.Id }, CancellationToken.None);
        }
    }

    [Benchmark]
    public async Task TestUpdate()
    {
        var sutCreate = new CreatePizzaCommandHandler(Context);
        var resultCreate = await sutCreate.Handle(PizzaTestData.Create, CancellationToken.None);

        if (!resultCreate.IsError)
        {
            var sutUpdate = new UpdatePizzaCommandHandler(Context);
            var resultUpdate = await sutUpdate.Handle(
                new UpdatePizzaCommand
                {
                    Id = resultCreate.Data.Id,
                    Model = new UpdatePizzaModel
                    {
                        Name = "Test"
                    }
                }, CancellationToken.None);
        }
    }

    [Benchmark]
    public async Task TestDelete()
    {
        var sutCreate = new CreatePizzaCommandHandler(Context);
        var resultCreate = await sutCreate.Handle(PizzaTestData.Create, CancellationToken.None);

        if (!resultCreate.IsError)
        {

            var sutDelete = new DeletePizzaCommandHandler(Context);
            var outcomeDelete = await sutDelete.Handle(
                new DeletePizzaCommand
                {
                    Id = resultCreate.Data.Id
                }, CancellationToken.None);
        }
    }
}
