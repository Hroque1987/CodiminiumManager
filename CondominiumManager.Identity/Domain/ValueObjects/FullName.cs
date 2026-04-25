using CondominiumManager.Identity.Domain.Errors;
using Sharedkernel.Results;


namespace CondominiumManager.Identity.Domain.ValueObjects;

public sealed record FullName
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;

    private const int MaxFirstNameLength = 50;
    private const int MaxLastNameLength = 50;

    private FullName() { }
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Result<FullName> Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return FullNameErrors.FirstNameEmpty;

        if (string.IsNullOrWhiteSpace(lastName))
            return FullNameErrors.LastNameEmpty;

        firstName = firstName.Trim();
        lastName = lastName.Trim();

        if (firstName.Length > MaxFirstNameLength)
            return FullNameErrors.FirstNameTooLong;

        if (lastName.Length > MaxLastNameLength)
            return FullNameErrors.LastNameTooLong;

        return new FullName(firstName, lastName);
    }

    public override string ToString() => $"{FirstName} {LastName}";
}
