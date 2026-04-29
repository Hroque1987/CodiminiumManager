using CondominiumManager.Condominium.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CondominiumManager.Condominium.Infrastructure;

public class CondominiumDbContext : DbContext
{
    internal DbSet<Building> Buildings { get; set; }
    public CondominiumDbContext(DbContextOptions<CondominiumDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Condominium");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

    }
}
