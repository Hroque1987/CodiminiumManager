using CondominiumManager.Finance.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CondominiumManager.Finance;

public static class FinanceDependencyInjection
{
    public static IServiceCollection AddFinance(this IServiceCollection services, ConfigurationManager configuration, ILogger logger)
    {
        string? connectionString = configuration.GetConnectionString("FinanceDb");
        services.AddDbContext<FinanceDbContext>(options => options.UseSqlServer(connectionString));

        logger.Information("{Module} module services registered", "Finance");

        return services;
    }
}
