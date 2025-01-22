namespace Common.StartupApp.App;

using System.Text.Json;
using Common.Logging.Static;
using Common.Middleware;
using Correlate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public static class CommonApp
{
    /// <summary>
    /// Adds the common.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <returns>IApplicationBuilder</returns>
    public static IApplicationBuilder AddCommon(this WebApplication app)
    {
        ////Exception Handling and Logging
        app.UseMiddleware<LoggingMiddleware>();

        ////COMPRESSION
        app.UseResponseCompression();
        app.UseHttpsRedirection();

        ////Correlation Id
        if (StartupSettings.Current.IncludeCorrelationId)
        {
            app.UseCorrelate();
        }

        ////SWAGGER
        app.UseOpenApi();
        app.UseSwaggerUi(c => c.AdditionalSettings.Add("displayRequestDuration", true));

        ////COMMON
        app.UseRouting();
        app.UseDefaultFiles();
        app.UseStaticFiles();

        ////Rate Limiting
        app.UseRateLimiter();

        ////Security
        if (StartupSettings.Current.IncludeAuthorization)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

        app.UseEndpoints(static endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async (context) =>
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/html";

                await context.Response.WriteAsync(GetHomePageHtml(StartupSettings.Current.DisplayName));
            });
            endpoints.MapGet("/hc",
                async ([FromServices] HealthCheckService healthCheckService, HttpContext context) =>
                {
                    var report = await healthCheckService.CheckHealthAsync();
                    Logger.LogInfo("Health check", Enum.GetName(report.Status));
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Status = Enum.GetName(report.Status),
                        Report = report
                    }));
                }).WithTags("Health Check");
        });

        return app;
    }

    private static string GetHomePageHtml(string title) => "<!DOCTYPE HTML><html>" +
            $"<head><meta name='viewport' content='width=device-width'/><title>{title}</title></head>" +
            "<body><div style='text-align:center;margin-top:15%;font-family:Arial'>.NET Vertical Slice Minimum API Template</div></body>" +
            "</html>";
}
