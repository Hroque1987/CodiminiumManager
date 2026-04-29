using CondominiumManager.Identity.Domain.Enums;
using CondominiumManager.Identity.Domain.ValueObjects;
using CondominiumManager.Identity.Errors;
using Sharedkernel.Results;
using Sharedkernel.Utils;

namespace CondominiumManager.Identity.Domain.Entities;

internal sealed class User : BaseEntity
{
    public const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$";
    public FullName Name { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public UserStatus Status { get; private set; }

    public string Password { get; private set; } = default!;

    private User() { }

    private User(FullName name, Email email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Status = UserStatus.Active;
        CreatedAt = DateTime.UtcNow;
        Password = password;
    }

    public static Result<User> Create(FullName name, Email email, string password)
    {
        ArgumentNullException.ThrowIfNull(name);

        ArgumentNullException.ThrowIfNull(email);

        if (string.IsNullOrWhiteSpace(password))
            return Result<User>.Failure(IdentityErrors.UserErrors.EmptyPassword);
        
        return Result<User>.Success(new User(name, email, password));
    }

    public static Result<User> Create(string firstName, string lastName, string email, string Password)
    {
        var emailResult = Email.Create(email);

        var fullNameResult = FullName.Create(firstName, lastName);

        if(emailResult.IsFailure || fullNameResult.IsFailure)
            return Result<User>.Failure([.. emailResult.Errors, .. fullNameResult.Errors]);

        return Create(fullNameResult.Value, emailResult.Value, Password);
    }

    public Result<Unit> ChangeEmail(Email newEmail)
    {
        if (Status == UserStatus.Inactive)
            return Result<Unit>.Failure(IdentityErrors.UserErrors.Inactive);


        Email = newEmail;
        SetUpdated();

        return Result<Unit>.Success(Unit.Value);
    }
    public Result<Unit> Inactivate()
    {
        if (Status == UserStatus.Inactive)
            return Result<Unit>.Failure(IdentityErrors.UserErrors.AlreadyInactive);

        Status = UserStatus.Inactive;
        SetUpdated();

        return Result<Unit>.Success(Unit.Value);
    }

    public Result<Unit> Activate()
    {
        if (Status == UserStatus.Active)
            return Result<Unit>.Failure(IdentityErrors.UserErrors.AlreadyActive); 

        Status = UserStatus.Active;
        SetUpdated();

        return Result<Unit>.Success(Unit.Value);
    }
}
