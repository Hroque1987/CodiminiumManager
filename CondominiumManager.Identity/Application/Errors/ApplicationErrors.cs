using Sharedkernel.Errors;


namespace CondominiumManager.Identity.Application.Errors;

internal static class ApplicationErrors
{

    public static readonly Error EmailAlreadyExists =
        Error.Conflict("EMAIL_REGISTERED", "Email already registered");


    public static readonly Error EmailUserNotFound =
        Error.Conflict("EMAIL_NOT_FOUND", "No User found with provided email");

    public static readonly Error InvalidLogin =
        Error.Conflict("INVALID_LOGIN", "Login Credentials privided invalid");
}

