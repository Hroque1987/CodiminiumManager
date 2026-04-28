using FastEndpoints;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace InfraStructure.FastEndPoints.PreProcessors;


public class LoggingPreProcessor<TRequest> : IPreProcessor<TRequest>
{
    private readonly ILogger<LoggingPreProcessor<TRequest>> _logger;

    public LoggingPreProcessor(ILogger<LoggingPreProcessor<TRequest>> logger)
    {
        _logger = logger;
    }

    public Task PreProcessAsync(IPreProcessorContext<TRequest> ctx, CancellationToken ct)
    {
        ctx.HttpContext.Items["StartTime"] = Stopwatch.GetTimestamp();

        _logger.LogInformation(
            "LOGGER Path: {Path} Method: {Method}",
            ctx.HttpContext.Request.Path,
            ctx.HttpContext.Request.Method);

        return Task.CompletedTask;
    }
}
