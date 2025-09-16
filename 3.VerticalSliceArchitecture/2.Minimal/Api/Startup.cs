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
        app.MapOpenApi("v1");

        // Add Scalar UI for OpenAPI visualization
        app.MapScalarApiReference("v1", config =>
        {
            config.Title = "Pezza API";
            config.Theme = ScalarTheme.BluePlanet;
        });

        app.AddSecurity();

        // Remove duplicate UseHttpsRedirection - it's already in AddCommon
        // app.UseHttpsRedirection();
    }
}