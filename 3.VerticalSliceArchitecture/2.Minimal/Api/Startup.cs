namespace Api;

using Api.Endpoints;
using Common.StartupApp.App;
using Common.StartupApp.Services;
using Core;
using Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static void RegisterServices(this WebApplicationBuilder builder)
        => builder.Services
        .AddEndpointsApiExplorer()
        .AddCommon()
        .AddSecurity()
        .AddApplication();

    public static void RegisterMiddlewares(this WebApplication app)
    {

        app.AddCommon();
        app.AddV1Endpoint();
        app.AddSecurity();

        app.UseHttpsRedirection();
    }
}