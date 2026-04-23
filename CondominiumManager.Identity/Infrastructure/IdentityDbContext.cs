using Microsoft.EntityFrameworkCore;

namespace CondominiumManager.Identity.Infrastructure;

public class IdentityDbContext : DbContext
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
