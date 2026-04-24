using Microsoft.Extensions.DependencyInjection;
using Sharedkernel.DomainEvents;
using InfraStructure.DomainEvents;

namespace InfraStructure;

public static class InfraStructureDependencyInjection
{
    public static IServiceCollection AddInfraStructure(this IServiceCollection services)
    {

        services.AddScoped<IDomainEventDispatcher, InMemoryDomainEventDispatcher>();

        return services;
    }
}
