using Sharedkernel.DomainEvents;

namespace CondominiumManager.Api.DevUtilities;

public class SendWelcomeEmailHandler
    : IDomainEventHandler<UserRegisteredEvent>
{
    private readonly ILogger<SendWelcomeEmailHandler> _logger;

    public SendWelcomeEmailHandler(ILogger<SendWelcomeEmailHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(UserRegisteredEvent @event, CancellationToken ct)
    {
        _logger.LogInformation("Email sent to {Email}", @event.Email);
        return Task.CompletedTask;
    }
}

