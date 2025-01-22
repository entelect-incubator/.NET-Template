namespace Core;

using System.Reflection;
using Common.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.Scan(scan =>
            scan.FromApplicationDependencies()
                .AddClasses(classes => classes.InNamespaces("DataAccess"))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }
}