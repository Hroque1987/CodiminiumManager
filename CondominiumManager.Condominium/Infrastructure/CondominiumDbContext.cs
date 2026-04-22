using Microsoft.EntityFrameworkCore;

namespace CondominiumManager.Condominium.Infrastructure;

internal class CondominiumDbContext : DbContext
{
    internal CondominiumDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Condominium");

    }
}
