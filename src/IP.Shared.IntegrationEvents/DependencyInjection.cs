using System.Reflection;

namespace IP.Shared.IntegrationEvents;

public static class DependencyInjection
{
    public static IServiceCollection AddInMemoryEventBus(
         this IServiceCollection services,
         Assembly[] assemblies)
    {
        services.AddScoped<IEventBus, InMemoryEventBus>();

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(
                typeof(IIntegrationEventHandler<>)), 
                publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}
