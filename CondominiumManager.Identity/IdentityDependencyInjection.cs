using CondominiumManager.Identity.Application;
using CondominiumManager.Identity.Application.Abstractions;
using CondominiumManager.Identity.Infrastructure;
using CondominiumManager.Identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Sharedkernel.Abstractions;

namespace CondominiumManager.Identity;

public static class IdentityDependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, ConfigurationManager configuration, ILogger logger)
    {
        string? connectionString = configuration.GetConnectionString("NotificationsDb");
        services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString, sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "Notifications")));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<RegisterUserHandler>();

        logger.Information("{Module} modules services registered", "Identity");

        return services;
    }
}
