using Sharedkernel.DomainEvents;

namespace CondominiumManager.Condominium.Domain.Entities;

public abstract class BaseEntity : EventStorage
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }

    protected void SetUpdated() => UpdatedAt = DateTime.UtcNow;

}
