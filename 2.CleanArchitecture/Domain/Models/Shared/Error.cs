namespace Domain.Models.Shared;

[ExcludeFromCodeCoverage]
public sealed class Error
{
    public string ErrorCode { get; set; }

    public string ErrorDescription { get; set; }
}
