using Sharedkernel.DomainEvents;

namespace CondominiumManager.Condominium.Domain.Events;

internal sealed record BuildingCreatedEvent(Guid BuildingId) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}