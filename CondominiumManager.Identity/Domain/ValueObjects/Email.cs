using CondominiumManager.Identity.Domain.Errors;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.Domain.ValueObjects;

internal sealed class Email
{
    public string Value { get; }

    private Email() { }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return EmailErrors.Empty;

        email = email.Trim().ToLowerInvariant();

        if (!IsValid(email))
            return EmailErrors.InvalidFormat;

        return new Email(email);
    }

    private static bool IsValid(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public override string ToString() => Value;
}
