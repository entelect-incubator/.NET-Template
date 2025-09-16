namespace Common.Middleware;

using System.Diagnostics;
using Common.Logging.Static;
using Microsoft.AspNetCore.Http;

[ExcludeFromCodeCoverage]
public class PerformanceLoggingMiddleware(RequestDelegate next)
{
    private readonly Stopwatch timer = new();

    public async Task Invoke(HttpContext context)
    {
        this.timer.Restart();

        await next(context);

        this.timer.Stop();
        var elapsedMilliseconds = this.timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            Logger.LogInfo("Performance", $"Long Running Request: {context.Request.Method} {context.Request.Path} ({elapsedMilliseconds} ms)");
        }
    }
}
