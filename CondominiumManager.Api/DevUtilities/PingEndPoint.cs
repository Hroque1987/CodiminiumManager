using FastEndpoints;
using Sharedkernel.DomainEvents;
internal record EmptyRecord(string message);
internal class PingEndPoint : EndpointWithoutRequest<EmptyRecord>
{
    private readonly IDomainEventDispatcher _dispatcher;

    public PingEndPoint(IDomainEventDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    public override void Configure()
    {
        Get("/ping");
        PostProcessor<LoggingPostProcessor<EmptyRecord>>();
        AllowAnonymous();
        
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var eventData = new UserRegisteredEvent("user@example.com", DateTime.UtcNow);
        await _dispatcher.DispatchAsync(eventData, ct);

        await Send.OkAsync(new EmptyRecord("Ping With Success"));
    }
}
