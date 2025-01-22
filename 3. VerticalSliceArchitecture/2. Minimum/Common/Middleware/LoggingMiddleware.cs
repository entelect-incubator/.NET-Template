namespace Common.Middleware;

using System.Net;
using System.Text;
using System.Text.Json;
using Common.Logging.Static;
using Common.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;

[ExcludeFromCodeCoverage]
public class LoggingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException operationCanceledException)
        {
            Logger.LogException(operationCanceledException);
            await HandleOperationCanceledExceptionAsync(context, operationCanceledException);
        }
        catch (ValidationException validationException)
        {
            Logger.LogException(validationException);
            await HandleValidationExceptionAsync(context, validationException);
        }
        catch (Exception ex)
        {
            Logger.LogException(ex);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleValidationExceptionAsync(HttpContext context, Exception exception)
    {
        var errors = ((ValidationException)exception).Errors;
        if (errors.Any())
        {
            var failures = errors.ToLookup(o => o.PropertyName.Replace("Data.", string.Empty), o => o.ErrorMessage.Replace("Data ", string.Empty));
            var result = Result.ValidationFailure(failures.ToDictionary(x => x.Key, x => x.ToList()));
            var code = HttpStatusCode.BadRequest;
            var resultJson = JsonSerializer.Serialize(result);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(resultJson);
        }
        else
        {
            var result = Result.Failure(exception?.Message);
            var resultJson = JsonSerializer.Serialize(result);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(resultJson);
        }
    }

    private static Task HandleOperationCanceledExceptionAsync(HttpContext context, Exception exception)
    {
        var result = JsonSerializer.Serialize(new { succeeded = false, errors = new List<object> { exception.Message } });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 499; ////499 Client Closed Request Used when the client has closed the request before the server could send a response.
        return context.Response.WriteAsync(result);
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var result = Result.Failure(exception?.Message);
        var resultJson = JsonSerializer.Serialize(result);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(resultJson);
    }

    private static async Task<string> FormatRequest(HttpRequest request)
    {
        request.EnableBuffering();
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadExactlyAsync(buffer);
        var bodyAsText = Encoding.UTF8.GetString(buffer);
        request.Body.Position = 0;
        return bodyAsText;
    }
}
