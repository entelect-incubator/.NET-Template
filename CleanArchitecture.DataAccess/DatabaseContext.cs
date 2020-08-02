namespace CleanArchitecture.DataAccess
{
    using CleanArchitecture.DataAccess.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext, IDatabaseContext
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
