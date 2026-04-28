namespace Sharedkernel.DomainEvents;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent @event, CancellationToken ct);

}
