using CondominiumManager.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CondominiumManager.Identity;

public static class IdentityDependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, ConfigurationManager configuration, ILogger logger)
    {
        string? connectionString = configuration.GetConnectionString("NotificationsDb");
        services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));

        logger.Information("{Module} modules services registered", "Identity");

        return services;
    }
}
