namespace Utilities.Behaviours;

using System.Diagnostics;
using MediatR;
using Utilities.Logging.Static;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch timer = new();

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        this.timer.Restart();

        var response = await next();

        this.timer.Stop();

        var elapsedMilliseconds = this.timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            Logger.LogInfo("PerformanceBehaviour", $"Long Running Request: {requestName} ({elapsedMilliseconds} milliseconds)");
        }

        return response;
    }
}