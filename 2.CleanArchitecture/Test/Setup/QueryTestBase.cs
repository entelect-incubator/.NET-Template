namespace Test.Setup;

using Infrastructure;
using static DatabaseContextFactory;

public class QueryTestBase : IDisposable
{
    public static DatabaseContext Context => Create();

    public void Dispose()
    {
        Destroy(Context);
        GC.SuppressFinalize(this);
    }
}
