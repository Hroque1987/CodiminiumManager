using CondominiumManager.Condominium.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CondominiumManager.Condominium;

public static class CondominiumDependencyInjection
{
    public static IServiceCollection AddCondominium(this IServiceCollection services, ConfigurationManager configuration, ILogger logger)
    {
        string? connectionString = configuration.GetConnectionString("CondominiumDb");
        services.AddDbContext<CondominiumDbContext>(options => options.UseSqlServer(connectionString));

        logger.Information("{Module} module services registered", "Condominium");

        return services;
    }
}
