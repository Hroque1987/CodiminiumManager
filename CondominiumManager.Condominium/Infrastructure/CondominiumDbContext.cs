using Microsoft.EntityFrameworkCore;

namespace CondominiumManager.Condominium.Infrastructure;

public class CondominiumDbContext : DbContext
{
    public CondominiumDbContext(DbContextOptions<CondominiumDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Condominium");

        base.OnModelCreating(modelBuilder);

    }
}
