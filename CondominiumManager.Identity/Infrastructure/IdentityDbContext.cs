using Microsoft.EntityFrameworkCore;

namespace CondominiumManager.Identity.Infrastructure;

internal class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Identity");

        base.OnModelCreating(modelBuilder);
    }
}
