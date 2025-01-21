namespace Utilities.Extensions;
public static class Extensions
{
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, PagingArgs pagingArgs)
    {
        var myPagingArgs = pagingArgs;

        if (pagingArgs == null)
        {
            myPagingArgs = PagingArgs.Default;
        }

        return myPagingArgs.UsePaging ? query.Skip(myPagingArgs.SkipAmount).Take(myPagingArgs.PageSize) : query;
    }

    public static IQueryable<T> ApplyPaging<T>(this List<T> query, PagingArgs pagingArgs)
    {
        var myPagingArgs = pagingArgs;

        if (pagingArgs == null)
        {
            myPagingArgs = PagingArgs.Default;
        }

        return query.ApplyPaging(myPagingArgs);
    }

    public static bool IsObjectNullOrEmpty(object myObject)
    {
        if (myObject == null)
        {
            return true;
        }

        foreach (var pi in myObject.GetType().GetProperties())
        {
            if (pi.PropertyType != typeof(string))
            {
                continue;
            }

            var value = pi.GetValue(myObject) as string;
            if (value == null || value.AsSpan().Trim().Length == 0)
            {
                return true;
            }
        }

        return false;
    }
}
