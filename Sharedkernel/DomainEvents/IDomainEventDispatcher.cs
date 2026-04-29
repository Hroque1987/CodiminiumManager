namespace Sharedkernel.DomainEvents;

public interface IDomainEventDispatcher
{
    Task DispatchAsync<TEvent>(TEvent @event, CancellationToken ct)where TEvent : IDomainEvent;

}
