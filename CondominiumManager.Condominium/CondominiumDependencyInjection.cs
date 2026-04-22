using CondominiumManager.Condominium.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CondominiumManager.Condominium;

public static class CondominiumDependencyInjection
{
    public static IServiceCollection AddCondominium(this IServiceCollection services, ConfigurationManager configuration)
    {
        string? connectionString = configuration.GetConnectionString("CondominiumManagerDb");
        services.AddDbContext<CondominiumDbContext>(options => options.UseSqlServer(connectionString));
        return services;
    }
}
