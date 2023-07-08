namespace Api.StartupApp.App;

using Correlate.AspNetCore;
using Api.Middleware;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public static class CommonApp
{
    /// <summary>
    /// Adds the common.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <returns>IApplicationBuilder</returns>
    public static IApplicationBuilder AddCommon(this IApplicationBuilder app)
    {
        ////Exception Handling and Logging
        app.UseMiddleware(typeof(LoggingMiddleware));

        ////COMPRESSION
        app.UseResponseCompression();
        app.UseHttpsRedirection();

        ////Correlation Id
        if (StartupSettings.Current.IncludeCorrelationId)
        {
            app.UseCorrelate();
        }

        ////SWAGGER
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "My API V1"));
        /*app.UseOpenApi();
         app.UseSwaggerUi3(c => c.AdditionalSettings.Add("displayRequestDuration", true));*/

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

        app.UseEndpoints(endpoints =>
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
                    Logger.LogInfo("Health check", Enum.GetName(typeof(HealthStatus), report.Status));
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Status = Enum.GetName(typeof(HealthStatus), report.Status),
                        Report = report
                    }));
                });
        });

        return app;
    }

    private static string GetHomePageHtml(string title) => "<!DOCTYPE HTML><html>" +
            $"<head><meta name='viewport' content='width=device-width'/><title>{title}</title></head>" +
            "<body><div style='text-align:center;margin-top:15%;font-family:Arial'>.NET Template</div></body>" +
            "</html>";
}
