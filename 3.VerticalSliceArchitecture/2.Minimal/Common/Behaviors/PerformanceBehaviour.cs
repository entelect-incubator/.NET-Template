namespace Common.Behaviors;

using System.Diagnostics;
using Common.Logging.Static;
using DispatchR.Abstractions.Send;

public class PerformanceBehavior<TRequest, TResponse>() : IPipelineBehavior<TRequest, Task<TResponse>>
    where TRequest : class, IRequest<TRequest, Task<TResponse>>
{
    private readonly Stopwatch timer = new();

    public IRequestHandler<TRequest, Task<TResponse>> NextPipeline { get; set; }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        this.timer.Restart();

        this.timer.Stop();

        var elapsedMilliseconds = this.timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            Logger.LogInfo("PerformanceBehavior", $"Long Running Request: {requestName} ({elapsedMilliseconds} milliseconds)");
        }

        return this.NextPipeline.Handle(request, cancellationToken);
    }
}