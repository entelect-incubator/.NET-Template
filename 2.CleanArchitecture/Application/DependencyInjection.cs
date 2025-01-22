namespace Application;

using Application.Pizzas.V1.Commands;
using Domain.Behaviours;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreatePizzaCommand>());

        return services;
    }
}