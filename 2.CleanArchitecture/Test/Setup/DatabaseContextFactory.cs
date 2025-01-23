namespace Test.Setup;

using Infrastructure;
using Microsoft.EntityFrameworkCore;

public class DatabaseContextFactory
{
    protected DatabaseContextFactory()
    {
    }

    public static DatabaseContext DBContextAsync()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase("MEMORYDB").Options;
        var dbContext = new DatabaseContext(options);
        Seed(dbContext);
        return dbContext;
    }

    public static DatabaseContext Create()
    {
        var context = DBContextAsync();

        context.Database.EnsureCreated();

        return context;
    }

    public static void Destroy(DatabaseContext context)
    {
        context.Database.EnsureDeleted();

        context.Dispose();
    }

    private static void Seed(DatabaseContext dbContext)
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        ////ADD SEED DATA

        dbContext.SaveChanges();
    }
}