namespace Test.Setup;

using global::Infrastructure;
using Infrastructure;
using static DatabaseContextFactory;

public class QueryTestBase : IDisposable
{
    public DatabaseContext Context => Create();

    public void Dispose() => Destroy(this.Context);
}