using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace CondominiumManager.Api.Middleware;

internal sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, Serilog.ILogger logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

      
        logger.Error(exception, "Unhandled exception occurred");

         var statusCode= exception switch
        {
            ApplicationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        httpContext.Response.StatusCode = statusCode;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = statusCode,
                Type = exception.GetType().Name,
                Title = "An error ocurred",
                Detail = exception.Message,
                
            }
        });
    }
}
