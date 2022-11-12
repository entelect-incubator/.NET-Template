namespace CleanArchitecture.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
        => this.Errors = new Dictionary<string, string[]>();

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        var failureGroups = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

        foreach (var failureGroup in failureGroups)
        {
            var propertyName = failureGroup.Key;
            var propertyFailures = failureGroup.ToArray();

            this.Errors.Add(propertyName, propertyFailures);
        }
    }

    public IDictionary<string, string[]> Errors { get; }
}