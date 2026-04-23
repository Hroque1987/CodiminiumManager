using Microsoft.EntityFrameworkCore;

namespace CondominiumManager.Finance.Infrastructure;

public class FinanceDbContext : DbContext
{
    public FinanceDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Finance");

        base.OnModelCreating(modelBuilder);
    }
}
