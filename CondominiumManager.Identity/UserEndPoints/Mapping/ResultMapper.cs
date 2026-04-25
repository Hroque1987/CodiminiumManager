using Microsoft.AspNetCore.Http;
using Sharedkernel.Errors;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.UserEndPoints.Mapping;

internal static class ResultMapper
{
    public static IResult? ToHttpError<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return null;

        var errors = result.Errors;

        var status = errors.MaxBy(e => e.Type)?.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
        var problem = new
        {
            errors = errors.Select(e => new
            {
                e.Code,
                e.Message
            })
        };

        return Results.Problem(
            title: "Request failed",
            statusCode: status,
            extensions: new Dictionary<string, object?>
            {
                ["errors"] = problem.errors
            });
    }

}


