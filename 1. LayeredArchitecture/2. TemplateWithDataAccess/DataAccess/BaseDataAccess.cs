namespace DataAccess;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class BaseDataAccess<TDatabaseContext>
{
    public BaseDataAccess(TDatabaseContext databaseContext)
        => this.DatabaseContext = databaseContext;

    protected TDatabaseContext DatabaseContext { get; }
}