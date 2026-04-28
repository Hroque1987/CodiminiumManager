using CondominiumManager.Identity.Domain.Errors;

namespace CondominiumManager.Identity.Domain.ValueObjects;

internal sealed record Email
{
    public string Value { get; private set; } = default!;

    private Email() { }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email), EmailErrors.Empty.Message); 

        email = email.Trim().ToLowerInvariant();

        if (!IsValid(email))
            throw new ArgumentException(EmailErrors.InvalidFormat.Message, nameof(email));

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
