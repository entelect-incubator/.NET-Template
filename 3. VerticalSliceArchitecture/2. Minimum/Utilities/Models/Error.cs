namespace Utilities.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public sealed class Error
{
    public string ErrorCode { get; set; }

    public string ErrorDescription { get; set; }
}
