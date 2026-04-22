using Microsoft.EntityFrameworkCore;

namespace CondominiumManager.Notifications.Infrastructure;

internal class NotificationsDbContext : DbContext
{
    public NotificationsDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Notifications");
    }
}
