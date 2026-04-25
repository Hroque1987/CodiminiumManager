using Sharedkernel.Errors;


namespace CondominiumManager.Identity.Infrastructure.Errors;

internal static class InfrastructureErrors
{
    public static readonly Error PersistanceError =
        Error.Domain("RECORD_PERSISTANCE_ERROR", "Error persisting record");

}

