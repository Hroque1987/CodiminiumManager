using CondominiumManager.Identity.Errors;
using Sharedkernel.Errors;
using Sharedkernel.Results;


namespace CondominiumManager.Identity.Domain.ValueObjects;

internal sealed record FullName
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;

    public const int MaxFirstNameLength = 50;
    public const int MaxLastNameLength = 50;

    private FullName() { }
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Result<FullName> Create(string firstName, string lastName)
    {
        var errors = new List<Error>();

       
        if (string.IsNullOrWhiteSpace(firstName))
        {
            errors.Add(IdentityErrors.FullNameErrors.FirstNameEmpty);
        }
        else
        {
            firstName = firstName.Trim();

            if (firstName.Length > MaxFirstNameLength)
                errors.Add(IdentityErrors.FullNameErrors.FirstNameTooLong);
        }


        if (string.IsNullOrWhiteSpace(lastName))
        {
            errors.Add(IdentityErrors.FullNameErrors.LastNameEmpty);
        }
        else
        {
            lastName = lastName.Trim();

            if (lastName.Length > MaxLastNameLength)
                errors.Add(IdentityErrors.FullNameErrors.LastNameTooLong);
        }

        if (errors.Count > 0)
            return Result<FullName>.Failure(errors);



        return Result<FullName>.Success(new FullName(firstName, lastName));
    }

    public override string ToString() => $"{FirstName} {LastName}";
}
