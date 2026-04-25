using Sharedkernel.Errors;


namespace CondominiumManager.Identity.Application.Errors;

internal static class ApplicationErrors
{

    public static readonly Error EmailAlreadyExists =
        Error.Conflict("EMAIL_REGISTERED", "Email already registered");
}

