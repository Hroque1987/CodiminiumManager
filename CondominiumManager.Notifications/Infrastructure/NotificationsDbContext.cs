using Microsoft.EntityFrameworkCore;

namespace CondominiumManager.Notifications.Infrastructure;

public class NotificationsDbContext : DbContext
{
    public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Notifications");

        base.OnModelCreating(modelBuilder);
    }
}
