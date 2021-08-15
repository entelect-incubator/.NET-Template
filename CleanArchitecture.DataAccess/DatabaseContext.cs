namespace CleanArchitecture.DataAccess
{
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }
    }
}
