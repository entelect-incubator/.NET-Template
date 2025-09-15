namespace Api.Endpoints;

using System.Reflection;
using Microsoft.Extensions.Logging;

public static class DependencyInjectionEndPoints
{
    // Scan all loaded assemblies for MapEndpoints methods
    public static void AddV1Endpoint(this IEndpointRouteBuilder app)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
            .ToArray();

        AddV1Endpoint(app, assemblies);
    }

    // Backward compatible API - accepts one or more assemblies
    public static void AddV1Endpoint(this IEndpointRouteBuilder app, params Assembly[] assemblies)
    {
        var loggerFactory = app.ServiceProvider.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
        var logger = loggerFactory?.CreateLogger(typeof(DependencyInjectionEndPoints));

        foreach (var assembly in assemblies)
        {
            try
            {
                logger?.LogInformation("Scanning assembly {Assembly}", assembly.FullName);

                var mapMethods = assembly.GetTypes()
                     .Where(t => t.IsClass && t.IsAbstract && t.IsSealed) // static classes
                     .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static))
                     .Where(m =>
                         m.Name == "MapEndpoints" &&
                         m.GetParameters().Length == 1 &&
                         m.GetParameters()[0].ParameterType == typeof(IEndpointRouteBuilder))
                     .ToList();

                if (mapMethods.Count == 0)
                {
                    logger?.LogDebug("No MapEndpoints methods found in {Assembly}", assembly.GetName().Name);
                    continue;
                }

                foreach (var method in mapMethods)
                {
                    try
                    {
                        logger?.LogInformation("Invoking MapEndpoints: {Method} in {Type}", method.Name, method.DeclaringType?.FullName);
                        method.Invoke(null, [app]);
                        logger?.LogInformation("Successfully invoked {Method}", method.Name);
                    }
                    catch (TargetInvocationException tie)
                    {
                        logger?.LogError(tie, "Error invoking MapEndpoints {Method} in assembly {Assembly}", method.Name, assembly.FullName);
                    }
                    catch (Exception ex)
                    {
                        logger?.LogError(ex, "Unexpected error invoking MapEndpoints {Method} in assembly {Assembly}", method.Name, assembly.FullName);
                    }
                }
            }
            catch (ReflectionTypeLoadException reflectionTypeLoadException)
            {
                logger?.LogError(reflectionTypeLoadException, "Failed to load types from assembly {Assembly}", assembly.FullName);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Failed scanning assembly {Assembly}", assembly.FullName);
            }
        }
    }
}
