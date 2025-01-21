namespace Utilities.Models;

[ExcludeFromCodeCoverage]
public static class ProcessEFResult<T1>
{
    public static Result<T1> Outcome(T1 model, int dbRowAffected, string errorMessage = "")
        => dbRowAffected > 0 ? Result<T1>.Success(model) : Result<T1>.Failure(string.IsNullOrEmpty(errorMessage) ? $"Error saving {typeof(T1).Name}" : errorMessage);
}

[ExcludeFromCodeCoverage]
public static class ProcessEFResult
{
    public static Result Outcome(int dbRowAffected, string errorMessage = "")
        => dbRowAffected > 0 ? Result.Success() : Result.Failure(errorMessage);
}
