namespace Core;

using System.Reflection;
using Common.Behaviors;
using Core.Pizzas.V1.Commands;
using DispatchR.Extensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    private const string DatabaseName = "DB";

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(builder => builder.UseInMemoryDatabase(DatabaseName));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddDispatchR(typeof(CreatePizzaCommand).Assembly, withPipelines: true, withNotifications: true);

        services.AddHealthChecks().AddDbContextCheck<DatabaseContext>();

        return services;
    }
}