namespace Utilities.Models;

using Utilities.Enums;
using Utilities.Extensions;

public class BaseSearchModel
{
    public string OrderBy { get; set; }

    public SortDirection SortDirection { get; set; }

    public string Ordering => $"{this.OrderBy} {this.SortDirection.GetDescription()}";

    public PagingArgs? PagingArgs { get; set; } = PagingArgs.Default;
}
