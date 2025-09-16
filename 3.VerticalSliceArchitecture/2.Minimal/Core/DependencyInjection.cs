namespace Core;

using System.Reflection;
using Core.Pizzas.V1.Queries;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    private const string DatabaseName = "DB";

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(builder => builder.UseInMemoryDatabase(DatabaseName));

        services.AddValidatorsFromAssemblyContaining<GetAllPizzasValidator>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAllServicesFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddHealthChecks().AddDbContextCheck<DatabaseContext>();

        return services;
    }

    public static IServiceCollection AddAllServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && (t.Name.EndsWith("Command") || t.Name.EndsWith("Query")))
            .ToList();

        foreach (var implType in serviceTypes)
        {
            var interfaceType = implType.GetInterface("I" + implType.Name);
            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, implType);
            }
        }

        return services;
    }
}