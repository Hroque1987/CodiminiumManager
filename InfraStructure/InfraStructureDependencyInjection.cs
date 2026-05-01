using InfraStructure.CurrentUser;
using InfraStructure.DomainEvents;
using InfraStructure.PasswordHasher;
using Microsoft.Extensions.DependencyInjection;
using Sharedkernel.Abstractions;
using Sharedkernel.DomainEvents;

namespace InfraStructure;

public static class InfraStructureDependencyInjection
{
    public static IServiceCollection AddInfraStructure(this IServiceCollection services)
    {

        services.AddScoped<IDomainEventDispatcher, InMemoryDomainEventDispatcher>();

        services.AddScoped<IPasswordService, PasswordService>();

        services.AddScoped<ICurrentUser, CurrentUserService>();

        return services;
    }
}
