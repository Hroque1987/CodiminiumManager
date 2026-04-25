using Sharedkernel.Errors;


namespace CondominiumManager.Identity.Domain.Errors;

internal static class OwnerErrors
{
    public static readonly Error AlreadyInactive =
        Error.Domain("OWNER_ALREADY_INACTIVE", "Owner is already inactive");

    public static readonly Error AlreadyActive =
        Error.Domain("OWNER_ALREADY_ACTIVE", "Owner is already active");

    public static readonly Error OwnerFullNameEmpty =
        Error.Domain("OWNER_FULL_NAME_EMPTY", "Owner full name is mandatory");

    public static readonly Error OwnerEmailEmpty =
        Error.Domain("OWNER_EMAIL_EMPTY", "Owner email is mandatory");

    public static readonly Error EmailSame =
        Error.Domain("OWNER_EMAIL_SAME", "Email is already set");

    public static readonly Error Inactive =
        Error.Domain("OWNER_INACTIVE", "Owner is incative");

    public static readonly Error EmptyPassword =
    Error.Domain("PASSWORD_EMPTY", "Password is empty");
}
