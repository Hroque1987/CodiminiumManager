using CondominiumManager.Identity.Domain.Entities;
using CondominiumManager.Identity.Errors;
using Sharedkernel.Errors;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.Domain.ValueObjects;

internal sealed record Email
{
    public string Value { get; private set; } = default!;

    private Email() { }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string email)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(email))
        {
            errors.Add(IdentityErrors.EmailErrors.Empty);
        }
        else
        {
            email = email.Trim().ToLowerInvariant();

            if (!IsValid(email))
                errors.Add(IdentityErrors.EmailErrors.InvalidFormat);
            
        }
            
       
        if(errors.Count > 0)
            return Result<Email>.Failure(errors);


        return Result<Email>.Success(new Email(email));
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
