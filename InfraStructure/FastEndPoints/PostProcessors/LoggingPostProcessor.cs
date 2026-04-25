using FastEndpoints;
using Microsoft.Extensions.Logging;

public class LoggingPostProcessor<TResponse> : IPostProcessor<EmptyRequest, TResponse>
{
    private readonly ILogger<LoggingPostProcessor<TResponse>> _logger;

    public LoggingPostProcessor(ILogger<LoggingPostProcessor<TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task PostProcessAsync(
        IPostProcessorContext<EmptyRequest, TResponse> ctx,
        CancellationToken ct)
    {
        _logger.LogInformation(
            "Endpoint: {Path} | Status: {StatusCode} | Response: {@Response}",
            ctx.HttpContext.Request.Path,
            ctx.HttpContext.Response.StatusCode,
            ctx.Response);
    }
}
