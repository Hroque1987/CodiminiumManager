using CondominiumManager.Identity.Domain.Errors;


namespace CondominiumManager.Identity.Domain.ValueObjects;

public sealed record FullName
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

    public static FullName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentNullException(nameof(firstName), FullNameErrors.FirstNameEmpty.Message);
       

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentNullException(nameof(lastName), FullNameErrors.LastNameEmpty.Message);
     

        firstName = firstName.Trim();
        lastName = lastName.Trim();

        if (firstName.Length > MaxFirstNameLength)
            throw new ArgumentException(FullNameErrors.FirstNameTooLong.Message, nameof(firstName));
       

        if (lastName.Length > MaxLastNameLength)
            throw new ArgumentException(FullNameErrors.LastNameTooLong.Message, nameof(lastName));
        

        return new FullName(firstName, lastName);
    }

    public override string ToString() => $"{FirstName} {LastName}";
}
