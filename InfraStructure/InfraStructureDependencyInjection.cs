using Microsoft.Extensions.DependencyInjection;
using Sharedkernel.DomainEvents;
using InfraStructure.DomainEvents;
using Sharedkernel.Abstractions;
using InfraStructure.PasswordHasher;

namespace InfraStructure;

public static class InfraStructureDependencyInjection
{
    public static IServiceCollection AddInfraStructure(this IServiceCollection services)
    {

        services.AddScoped<IDomainEventDispatcher, InMemoryDomainEventDispatcher>();

        services.AddScoped<IPasswordService, PasswordService>();

        return services;
    }
}
