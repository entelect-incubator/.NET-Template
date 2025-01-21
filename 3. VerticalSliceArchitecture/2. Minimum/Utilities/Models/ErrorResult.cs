namespace Utilities.Models;

using System.ComponentModel;

[ExcludeFromCodeCoverage]
public class ErrorResult : Result
{
    public ErrorResult()
        => (this.IsError, this.IsValidationError) = (true, false);

    [DefaultValue(true)]
    public new bool IsError { get; set; }

    [DefaultValue(false)]
    public new bool IsValidationError { get; set; }
}

[ExcludeFromCodeCoverage]
public class NotFoundErrorResult : Result
{
    public NotFoundErrorResult()
        => this.IsNotFound = true;

    [DefaultValue(true)]
    public new bool IsNotFound { get; set; }
}

[ExcludeFromCodeCoverage]
public class ValidationErrorResult : Result
{
    public ValidationErrorResult()
        => (this.IsError, this.IsValidationError) = (false, true);

    [DefaultValue(false)]
    public new bool IsError { get; set; }

    [DefaultValue(true)]
    public new bool IsValidationError { get; set; }
}