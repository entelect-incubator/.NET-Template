namespace Test.Setup;

using global::Core;
using static DatabaseContextFactory;

public class QueryTestBase : IDisposable
{
    public static DatabaseContext Context => Create();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Destroy(Context);
    }
}