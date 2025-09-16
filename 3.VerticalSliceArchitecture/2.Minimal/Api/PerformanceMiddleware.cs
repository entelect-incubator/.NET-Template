using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Common.Logging.Static;

public class PerformanceMiddleware
{
    private readonly RequestDelegate _next;

    public PerformanceMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var timer = Stopwatch.StartNew();
        await _next(context);
        timer.Stop();

        var elapsedMilliseconds = timer.ElapsedMilliseconds;
        if (elapsedMilliseconds > 500)
        {
            var endpoint = context.GetEndpoint()?.DisplayName ?? context.Request.Path;
            Logger.LogInfo("PerformanceMiddleware", $"Long Running Request: {endpoint} ({elapsedMilliseconds} ms)");
        }
    }
}
