namespace Utilities.Models;

[ExcludeFromCodeCoverage]
public class Result
{
    public bool IsError { get; set; } = false;

    public bool IsValidationError { get; set; } = false;

    public string Message { get; set; }

    public List<string> Errors { get; set; } = [];

    public Dictionary<string, List<string>> ValidationErrors { get; set; } = [];

    public static Result Success() => new()
    {
        IsError = false,
        Errors = [],
        ValidationErrors = []
    };

    public static Result Success(string message) => new()
    {
        IsError = false,
        Message = message,
        Errors = [],
        ValidationErrors = []
    };

    public static Result Failure(List<string> errors) => new()
    {
        IsError = true,
        Errors = errors,
        ValidationErrors = []
    };

    public static Result Failure(List<string> errors, string message) => new()
    {
        IsError = true,
        Errors = errors,
        Message = message,
        ValidationErrors = []
    };

    public static Result Failure(string error) => new()
    {
        IsError = true,
        Errors =
        [
            error,
        ],
        ValidationErrors = []
    };

    public static Result Failure(string error, string message) => new()
    {
        IsError = true,
        Message = message,
        Errors =
        [
            error,
        ],
        ValidationErrors = []
    };

    public static Result ValidationFailure(Dictionary<string, List<string>> validationErrors) => new()
    {
        IsValidationError = true,
        Errors = [],
        ValidationErrors = validationErrors
    };

    public static Result ValidationFailure(Dictionary<string, List<string>> validationErrors, string message) => new()
    {
        IsValidationError = true,
        Message = message,
        Errors = [],
        ValidationErrors = validationErrors
    };

    public static Result NotFound() => new()
    {
        IsError = false,
        IsValidationError = false
    };

    public static Result NotFound(string message) => new()
    {
        IsError = false,
        IsValidationError = false,
        Message = message
    };

    public void AddError(string error)
    {
        this.IsError = true;
        this.Errors.Add(error);
    }

    public void AddValidationError(string name, List<string> errors)
    {
        this.IsValidationError = true;

        if (this.ValidationErrors.TryGetValue(name, out var value))
        {
            value.AddRange(errors);
        }
        else
        {
            this.ValidationErrors.Add(name, errors);
        }
    }
}

[ExcludeFromCodeCoverage]
public class Result<T>
{
    public bool IsError { get; set; } = false;

    public bool IsValidationError { get; set; } = false;

    public T Data { get; set; }

    public int Count { get; set; }

    public string Message { get; set; }

    public List<string> Errors { get; set; } = [];

    public Dictionary<string, List<string>> ValidationErrors { get; set; } = [];

    public static Result<T> Success(T data, int count = 0) => new()
    {
        Data = data,
        Count = count,
        IsError = false
    };

    public static Result<T> Success(T data, string message, int count = 0) => new()
    {
        Data = data,
        IsError = false,
        Count = count,
        Message = message
    };

    public static Result<T> Failure(string error) => new()
    {
        IsError = true,
        Errors = [error]
    };

    public static Result<T> Failure(List<string> errors) => new()
    {
        IsError = true,
        Errors = errors
    };

    public static Result<T> Failure(List<string> errors, string message) => new()
    {
        IsError = true,
        Errors = errors,
        Message = message
    };

    public static Result<T> Failure(string error, string message) => new()
    {
        IsError = true,
        Message = message,
        Errors =
        [
            error,
        ]
    };

    public static Result<T> ValidationFailure(string validationError) => new()
    {
        IsValidationError = true,
        Message = validationError
    };

    public static Result<T> ValidationFailure(Dictionary<string, List<string>> validationErrors) => new()
    {
        IsValidationError = true,
        ValidationErrors = validationErrors
    };

    public static Result<T> ValidationFailure(Dictionary<string, List<string>> validationErrors, T data = default) => new()
    {
        Data = data,
        IsValidationError = true,
        ValidationErrors = validationErrors
    };

    public static Result<T> ValidationFailure(Dictionary<string, List<string>> validationErrors, string message, T data = default) => new()
    {
        Data = data,
        Message = message,
        IsValidationError = true,
        ValidationErrors = validationErrors
    };

    public static Result<T> NotFound() => new()
    {
        IsError = false,
        IsValidationError = false
    };

    public static Result<T> NotFound(string message) => new()
    {
        IsError = false,
        IsValidationError = false,
        Message = message
    };

    public void AddData(T data)
        => this.Data = data;

    public void AddError(string error)
    {
        this.IsError = true;
        this.Errors.Add(error);
    }

    public void AddValidationError(string name, List<string> errors)
    {
        this.IsValidationError = true;
        if (this.ValidationErrors.ContainsKey(name))
        {
            this.ValidationErrors[name].AddRange(errors);
        }
        else
        {
            this.ValidationErrors.Add(name, errors);
        }
    }
}