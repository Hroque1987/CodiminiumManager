namespace Sharedkernel.DomainEvents;

public record UserRegisteredEvent(string Email, DateTime RegisteredAt) : IDomainEvent;