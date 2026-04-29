using CondominiumManager.Identity.Domain.Entities;
using CondominiumManager.Identity.Domain.ValueObjects;
using Sharedkernel.Errors;

namespace CondominiumManager.Identity.Errors;

internal class IdentityErrors
{
    internal static class EmailErrors
    {
        public static readonly Error Empty =
            Error.Domain("EMAIL_EMPTY", "Email cannot be empty");

        public static readonly Error InvalidFormat =
            Error.Domain("EMAIL_INVALID", "Email format is invalid");
    }

    internal static class FullNameErrors
    {
        public static readonly Error FirstNameEmpty =
            Error.Domain("FIRST_NAME_EMPTY", "First name cannot be empty");

        public static readonly Error LastNameEmpty =
            Error.Domain("LAST_NAME_EMPTY", "Last name cannot be empty");

        public static readonly Error FirstNameTooLong =
            Error.Domain("FIRST_NAME_TOO_LONG", $"Fisrt name cannot exceed {FullName.MaxFirstNameLength} characters"); 

        public static readonly Error LastNameTooLong =
            Error.Domain("LAST_NAME_TOO_LONG", $"Last name cannot exceed {FullName.MaxLastNameLength} characters");
    }

    internal static class UserErrors
    {
        public static readonly Error AlreadyInactive =
            Error.Domain("OWNER_ALREADY_INACTIVE", "Owner is already inactive");

        public static readonly Error AlreadyActive =
            Error.Domain("OWNER_ALREADY_ACTIVE", "Owner is already active");

        public static readonly Error UserFullNameEmpty =
            Error.Domain("USER_FULL_NAME_EMPTY", "User full name is mandatory");

        public static readonly Error UserEmailEmpty =
            Error.Domain("USER_EMAIL_EMPTY", "User email is mandatory");

        public static readonly Error EmailSame =
            Error.Domain("USER_EMAIL_SAME", "Email is already set");

        public static readonly Error Inactive =
            Error.Domain("USER_INACTIVE", "User is inactive");

        public static readonly Error EmptyPassword =
            Error.Domain("PASSWORD_EMPTY", "Password is empty");


    }


    internal static class LoginErrors
    {
        public static readonly Error InvalidLogin =
            Error.Authentication("INVALID_LOGIN", "Login Credentials provided invalid");

    }
}