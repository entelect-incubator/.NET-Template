namespace CleanArchitecture.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch timer;

    public PerformanceBehaviour() => timer = new Stopwatch();

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        timer.Start();

        var response = await next();

        timer.Stop();

        var elapsedMilliseconds = timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            Logging.Logging.LogInfo($"CleanArchitecture Long Running Request: {typeof(TRequest).Name} ({elapsedMilliseconds} milliseconds)k", request);
        }

        return response;
    }
}