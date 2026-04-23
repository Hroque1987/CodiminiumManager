using FastEndpoints;
internal record EmptyRecord(string message);
internal class PingEndPoint : EndpointWithoutRequest<EmptyRecord>
{
    public override void Configure()
    {
        Get("/ping");
        PostProcessor<LoggingPostProcessor<EmptyRecord>>();
        AllowAnonymous();
        
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.OkAsync(new EmptyRecord("Ping With Success"));
    }
}
