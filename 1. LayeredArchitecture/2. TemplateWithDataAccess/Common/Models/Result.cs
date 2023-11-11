namespace Common.Models;
[ExcludeFromCodeCoverage]
public class Result
{
    public bool IsError { get; set; } = false;

    public bool IsValidationError { get; set; } = false;

    public string Message { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    public Dictionary<string, List<string>> ValidationErrors { get; set; } = new Dictionary<string, List<string>>();

    public static Result Success() => new()
    {
        IsError = false,
        Errors = new List<string>(),
        ValidationErrors = new Dictionary<string, List<string>>()
    };

    public static Result Success(string message) => new()
    {
        IsError = false,
        Message = message,
        Errors = new List<string>(),
        ValidationErrors = new Dictionary<string, List<string>>()
    };

    public static Result Failure(List<string> errors) => new()
    {
        IsError = true,
        Errors = errors,
        ValidationErrors = new Dictionary<string, List<string>>()
    };

    public static Result Failure(List<string> errors, string message) => new()
    {
        IsError = true,
        Errors = errors,
        Message = message,
        ValidationErrors = new Dictionary<string, List<string>>()
    };

    public static Result Failure(string error) => new()
    {
        IsError = true,
        Errors = new List<string>()
        {
            error,
        },
        ValidationErrors = new Dictionary<string, List<string>>()
    };

    public static Result Failure(string error, string message) => new()
    {
        IsError = true,
        Message = message,
        Errors = new List<string>()
        {
            error,
        },
        ValidationErrors = new Dictionary<string, List<string>>()
    };

    public static Result ValidationFailure(Dictionary<string, List<string>> validationErrors) => new()
    {
        IsValidationError = true,
        Errors = new List<string>(),
        ValidationErrors = validationErrors
    };

    public static Result ValidationFailure(Dictionary<string, List<string>> validationErrors, string message) => new()
    {
        IsValidationError = true,
        Message = message,
        Errors = new List<string>(),
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

[ExcludeFromCodeCoverage]
public class Result<T>
{
    public bool IsError { get; set; } = false;

    public bool IsValidationError { get; set; } = false;

    public T Data { get; set; }

    public string Message { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    public Dictionary<string, List<string>> ValidationErrors { get; set; } = new Dictionary<string, List<string>>();

    public static Result<T> Success(T data) => new()
    {
        Data = data,
        IsError = false
    };

    public static Result<T> Success(T data, string message) => new()
    {
        Data = data,
        IsError = false,
        Message = message
    };

    public static Result<T> Failure(string error) => new()
    {
        IsError = true,
        Errors = new List<string> { error }
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
        Errors = new List<string>()
        {
            error,
        }
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

[ExcludeFromCodeCoverage]
public class ListResult<T>
{
    public bool IsError { get; set; } = false;

    public bool IsValidationError { get; set; } = false;

    public List<T> Data { get; set; }

    public int Count { get; set; } = 0;

    public string Message { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    public Dictionary<string, List<string>> ValidationErrors { get; set; } = new Dictionary<string, List<string>>();

    public static ListResult<T> Success(List<T> data, int count) => new()
    {
        Data = data,
        Count = count,
        IsError = false
    };

    public static ListResult<T> Success(List<T> data, int count, string message) => new()
    {
        Data = data,
        Count = count,
        IsError = false,
        Message = message
    };

    public static ListResult<T> Failure(List<string> errors, List<T>? data = null, int count = 0) => new()
    {
        Data = data,
        Count = count,
        IsError = true,
        Errors = errors
    };

    public static ListResult<T> Failure(List<string> errors, string message, List<T>? data = null, int count = 0) => new()
    {
        Data = data,
        Count = count,
        IsError = true,
        Errors = errors,
        Message = message
    };

    public static ListResult<T> Failure(string error, List<T>? data = null, int count = 0) => new()
    {
        Data = data,
        Count = count,
        IsError = true,
        Errors = new List<string>()
        {
            error,
        }
    };

    public static ListResult<T> Failure(string error, string message, List<T>? data = null, int count = 0) => new()
    {
        Data = data,
        Count = count,
        IsError = true,
        Message = message,
        Errors = new List<string>()
        {
            error,
        }
    };

    public static ListResult<T> ValidationFailure(string validationError, List<T>? data = null, int count = 0) => new()
    {
        Data = data,
        Count = count,
        IsValidationError = true,
        Message = validationError
    };

    public static ListResult<T> ValidationFailure(Dictionary<string, List<string>> validationErrors, List<T>? data = null, int count = 0) => new()
    {
        Data = data,
        Count = count,
        IsValidationError = true,
        ValidationErrors = validationErrors
    };

    public static ListResult<T> ValidationFailure(Dictionary<string, List<string>> validationErrors, string message, List<T>? data = null, int count = 0) => new()
    {
        Data = data,
        Count = count,
        Message = message,
        IsValidationError = true,
        ValidationErrors = validationErrors
    };

    public void AddData(List<T> data, int count)
        => (this.Data, this.Count) = (data, count);

    public void AddError(string error)
    {
        this.IsError = true;
        this.Errors.Add(error);
    }

    public void AddValidationError(string name, List<string> errors)
    {
        this.IsValidationError = true;
        this.ValidationErrors.Add(name, errors);
    }
}