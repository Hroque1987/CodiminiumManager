using CondominiumManager.Notifications.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CondominiumManager.Notifications;

public static class NotificationsDependencyInjection
{
    public static IServiceCollection AddNotifications(this IServiceCollection services, ConfigurationManager configuration, ILogger logger)
    {
        string? connectionString = configuration.GetConnectionString("IdentityDb");
        services.AddDbContext<NotificationsDbContext>(options => options.UseSqlServer(connectionString, sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "Notifications")));


        logger.Information("{Module} modules services registered", "Notifications");

        return services;
    }
}
