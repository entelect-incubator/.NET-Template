namespace Api;

using Core;
using Core.Pizzas.V1.Endpoints;
using Microsoft.Extensions.DependencyInjection;
using Common.StartupApp.App;
using Common.StartupApp.Services;

public static class Startup
{
    public static void RegisterServices(this WebApplicationBuilder builder)
        => builder.Services
        .AddCommon()
        .AddSecurity()
        .AddApplication()
        .AddEndpointsApiExplorer();

    public static void RegisterMiddlewares(this WebApplication app)
    {
        app.AddCommon();
        app.AddSecurity();

        app.AddV1Endpoints();
        app.UseHttpsRedirection();
    }
}