using CondominiumManager.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CondominiumManager.Identity.Infrastructure;

public class IdentityDbContext : DbContext
{
    internal DbSet<User> Users { get; set; }
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
        
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Identity");
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
