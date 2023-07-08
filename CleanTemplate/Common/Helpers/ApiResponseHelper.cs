namespace Common.Helpers;

using Common.Models.Shared;
using Microsoft.AspNetCore.Mvc;

public static class ApiResponseHelper
{
    public static ActionResult<Result<T>> ResponseOutcome<T>(Result<T> result, ControllerBase controller)
        => result switch
        {
            { IsValidationError: false, Data: null } and { IsError: false } => controller.NotFound(result),
            { IsError: true } => controller.StatusCode(500, result),
            { IsValidationError: true } => controller.BadRequest(result),
            _ => controller.Ok(result)
        };

    public static ActionResult<ListResult<T>> ResponseOutcome<T>(ListResult<T> result, ControllerBase controller)
        => result switch
        {
            { IsError: true } => controller.StatusCode(500, result),
            { IsValidationError: true } => controller.BadRequest(result),
            _ => controller.Ok(result)
        };

    public static ActionResult<Result> ResponseOutcome(Result result, ControllerBase controller)
        => result switch
        {
            { IsError: true } => controller.StatusCode(500, result),
            { IsValidationError: true } => controller.BadRequest(result),
            _ => controller.Ok(result)
        };
}
