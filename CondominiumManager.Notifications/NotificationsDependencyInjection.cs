using CondominiumManager.Notifications.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CondominiumManager.Notifications;

public static class NotificationsDependencyInjection
{
    public static IServiceCollection AddNotifications(this IServiceCollection services, ConfigurationManager configuration)
    {
        string? connectionString = configuration.GetConnectionString("CondominiumManagerDb");
        services.AddDbContext<NotificationsDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}
