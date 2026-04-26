using FastEndpoints;
using InfraStructure.FastEndPoints.PostProcessors;
using InfraStructure.FastEndPoints.PreProcessors;
using Microsoft.Extensions.Diagnostics.HealthChecks;

internal class Health : EndpointWithoutRequest<HealthResponse>
{
  

 
    public override void Configure()
    {
        Get("/health");
        PreProcessor<LoggingPreProcessor<EmptyRequest>>();
        PostProcessor<LoggingPostProcessor<EmptyRequest, HealthResponse>>();
        Policies("HealthCheck");
        
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      
        

        await Send.OkAsync(new HealthResponse());
    }
}
