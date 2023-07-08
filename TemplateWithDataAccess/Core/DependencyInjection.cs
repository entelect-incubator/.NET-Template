namespace Core;

using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Common.Behaviours;

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