using Sharedkernel.Errors;


namespace CondominiumManager.Identity.Infrastructure.Errors;

internal static class ApplicationErrors
{
    public static readonly Error PersistanceError =
        Error.Infrastructure("RECORD_PERSISTANCE_ERROR", "Error persisting record");

}

