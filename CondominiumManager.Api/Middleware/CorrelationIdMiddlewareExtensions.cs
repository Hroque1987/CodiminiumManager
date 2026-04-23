namespace CondominiumManager.Api.Middleware;

public static class CorrelationIdMiddlewareExtensions
{
    public static IApplicationBuilder UseCorrelation(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<CorrelationIdMiddleware>();
        return builder;
    }
}
