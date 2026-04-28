using FastEndpoints;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace InfraStructure.FastEndPoints.PostProcessors;

public class LoggingPostProcessor<TRequest, TResponse> : IPostProcessor<TRequest, TResponse>
{
    private readonly ILogger<LoggingPostProcessor<TRequest, TResponse>> _logger;

    public LoggingPostProcessor(ILogger<LoggingPostProcessor<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public Task PostProcessAsync(IPostProcessorContext<TRequest, TResponse> ctx, CancellationToken ct)
    {
        var status = ctx.HttpContext.Response.StatusCode;

        var start = (long?)ctx.HttpContext.Items["StartTime"];
        var elapsedMs = start.HasValue
            ? (Stopwatch.GetTimestamp() - start.Value) / (double)TimeSpan.TicksPerMillisecond
            : 0;

        var path = ctx.HttpContext.Request.Path;

        var method = ctx.HttpContext.Request.Method;



        _logger.LogInformation("LOGGER Method: {Mthod} Endpoint: {Path} Status: {StatusCode} Elapsed: {Elapsed}ms",
                               method,
                               path,
                               status,
                               elapsedMs);

        return Task.CompletedTask;
    }
}
