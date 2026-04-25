using FastEndpoints;
using Sharedkernel.DomainEvents;
internal record EmptyRecord(string message);
internal class Health : EndpointWithoutRequest<EmptyRecord>
{
    private readonly IDomainEventDispatcher _dispatcher;

    public Health(IDomainEventDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    public override void Configure()
    {
        Get("/health");
        PostProcessor<LoggingPostProcessor<EmptyRecord>>();
        AllowAnonymous();
        
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var eventData = new UserRegisteredEvent("user@example.com", DateTime.UtcNow);
        await _dispatcher.DispatchAsync(eventData, ct);

        await Send.OkAsync();
    }
}
