using CondominiumManager.Condominium.Application.Abstractions;
using CondominiumManager.Condominium.Application.Usecases;
using CondominiumManager.Condominium.Infrastructure;
using CondominiumManager.Condominium.Infrastructure.Repositories;
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
        services.AddDbContext<CondominiumDbContext>(options => options.UseSqlServer(connectionString, sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "Condominium")));

        services.AddScoped<IBuildingRepository, BuildingRepository>();

        services.AddScoped<CreateBuildingHandler>();

        services.AddAuthorizationBuilder()
            .AddPolicy("Building", policy =>
            policy.RequireClaim("permission", Permissions.CreateBuilding));


        logger.Information("{Module} module services registered", "Condominium");

        return services;
    }
}
