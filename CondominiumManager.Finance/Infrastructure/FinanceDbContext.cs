using Microsoft.EntityFrameworkCore;

namespace CondominiumManager.Finance.Infrastructure;

internal class FinanceDbContext : DbContext
{
    internal FinanceDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Finance");
    }
}
