using Sharedkernel.Errors;


namespace CondominiumManager.Identity.Domain.Errors;

internal static class FullNameErrors
{
    public static readonly Error FirstNameEmpty =
        Error.Domain("FIRST_NAME_EMPTY", "First name cannot be empty");

    public static readonly Error LastNameEmpty =
        Error.Domain("LAST_NAME_EMPTY", "Last name cannot be empty");

    public static readonly Error FirstNameTooLong =
        Error.Domain("FIRST_NAME_TOO_LONG", "First name is too long");

    public static readonly Error LastNameTooLong =
        Error.Domain("LAST_NAME_TOO_LONG", "Last name is too long");
}