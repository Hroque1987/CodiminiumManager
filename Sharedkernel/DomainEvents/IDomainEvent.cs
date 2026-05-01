namespace Sharedkernel.DomainEvents;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
