namespace Test.Setup;

using Features;
using static DatabaseContextFactory;

public class QueryTestBase : IDisposable
{
    public DatabaseContext Context => Create();

    public void Dispose() => Destroy(this.Context);
}