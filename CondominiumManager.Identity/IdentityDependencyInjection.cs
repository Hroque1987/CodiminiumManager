using CondominiumManager.Identity.Application.Abstractions;
using CondominiumManager.Identity.Application.Auth;
using CondominiumManager.Identity.Application.UseCases;
using CondominiumManager.Identity.Infrastructure;
using CondominiumManager.Identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CondominiumManager.Identity;

public static class IdentityDependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, ConfigurationManager configuration, ILogger logger)
    {
        string? connectionString = configuration.GetConnectionString("IdentityDb");
        services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString, sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "Notifications")));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<RegisterUserHandler>();

        services.AddScoped<LoginUserHandler>();

       

        services.AddScoped<IdentityService>();

        var jwtConfig = configuration.GetSection("JwtSettings").Get<JwtConfiguration>();

        services.AddSingleton(jwtConfig!);

        services.AddJwtAuthentication(jwtConfig!);






        logger.Information("{Module} modules services registered", "Identity");

        return services;
    }
}
