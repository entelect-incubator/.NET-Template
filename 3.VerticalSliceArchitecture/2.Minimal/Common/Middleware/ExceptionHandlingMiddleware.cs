namespace Common.Middleware;

using System.Net;
using System.Text.Json;
using Common.Logging.Static;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ExcludeFromCodeCoverage]
public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException ex)
        {
            Logger.LogException(ex);
            await WriteProblemDetailsAsync(context, new ProblemDetails
            {
                Type = "https://yourdomain/errors/client-cancelled",
                Title = "Request cancelled by client",
                Detail = ex.Message,
                Status = 499
            });
        }
        catch (ValidationException ex)
        {
            Logger.LogException(ex);

            var errors = ex.Errors
                .GroupBy(e => e.PropertyName.Replace("Data.", string.Empty))
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage.Replace("Data ", string.Empty)).ToArray());

            var problem = new ValidationProblemDetails(errors)
            {
                Type = "https://yourdomain/errors/validation",
                Title = "Validation failed",
                Detail = "One or more validation errors occurred.",
                Status = (int)HttpStatusCode.BadRequest
            };

            await WriteProblemDetailsAsync(context, problem);
        }
        catch (Exception ex)
        {
            Logger.LogException(ex);
            await WriteProblemDetailsAsync(context, new ProblemDetails
            {
                Type = "https://yourdomain/errors/unexpected",
                Title = "An unexpected error occurred",
                Detail = ex.Message,
                Status = (int)HttpStatusCode.InternalServerError
            });
        }
    }

    private static Task WriteProblemDetailsAsync(HttpContext context, ProblemDetails problem)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problem.Status ?? (int)HttpStatusCode.InternalServerError;
        var json = JsonSerializer.Serialize(problem);
        return context.Response.WriteAsync(json);
    }
}
