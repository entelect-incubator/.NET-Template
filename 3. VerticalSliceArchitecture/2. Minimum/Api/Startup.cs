namespace Api;

using Api.StartupApp.App;
using Api.StartupApp.Services;
using Core;
using Core.Pizzas.V1.Endpoints;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Settings = Utilities.Settings;

public static class Startup
{
    public static void RegisterServices(this WebApplicationBuilder builder) => builder.Services
        .AddCommon()
        .AddSecurity()
        .AddApplication()
        .AddEndpointsApiExplorer()
        .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("Pezza"))
        .WithMetrics(metrics =>
        {
            metrics
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation();

            metrics.AddOtlpExporter(options => options.Endpoint = new Uri(Settings.Current.OpenTelemetryExportUrl));
        })
        .WithTracing(tracing =>
        {
            tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation();

            tracing.AddOtlpExporter(options => options.Endpoint = new Uri(Settings.Current.OpenTelemetryExportUrl));
        });

    public static void RegisterMiddlewares(this WebApplication app)
    {
        app.AddCommon();
        app.AddSecurity();

        app.AddV1Endpoints();
        app.UseHttpsRedirection();
    }
}