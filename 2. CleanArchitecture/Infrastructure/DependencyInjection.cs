namespace Infrastructure;

using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    private const string DatabaseName = "DB";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(builder => builder.UseInMemoryDatabase(DatabaseName));
        services.AddHealthChecks().AddDbContextCheck<DatabaseContext>();

        return services;
    }
}