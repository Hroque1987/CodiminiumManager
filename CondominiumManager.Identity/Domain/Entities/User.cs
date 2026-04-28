using CondominiumManager.Identity.Domain.Enums;
using CondominiumManager.Identity.Domain.Errors;
using CondominiumManager.Identity.Domain.ValueObjects;
using Sharedkernel.Results;
using Sharedkernel.Utils;

namespace CondominiumManager.Identity.Domain.Entities;

internal sealed class User : BaseEntity
{
    public FullName Name { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public OwnerStatus Status { get; private set; }

    public string Password { get; private set; } = default!;

    private User() { }

    private User(FullName name, Email email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Status = OwnerStatus.Active;
        CreatedAt = DateTime.UtcNow;
        Password = password;
    }

    public static User Create(FullName name, Email email, string password)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name), UserErrors.OwnerFullNameEmpty.Message);
           
        if (email is null)
            throw new ArgumentNullException(nameof(email), UserErrors.OwnerEmailEmpty.Message);
      
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password), UserErrors.EmptyPassword.Message);

        return new User(name, email, password);
    }

    public Result<Unit> ChangeEmail(Email newEmail)
    {
        if (Status == OwnerStatus.Inactive)
            return Result<Unit>.Failure(UserErrors.Inactive);


        Email = newEmail;
        SetUpdated();

        return Result<Unit>.Success(Unit.Value);
    }
    public Result<Unit> Inactivate()
    {
        if (Status == OwnerStatus.Inactive)
            return Result<Unit>.Failure(UserErrors.AlreadyInactive);

        Status = OwnerStatus.Inactive;
        SetUpdated();

        return Result<Unit>.Success(Unit.Value);
    }

    public Result<Unit> Activate()
    {
        if (Status == OwnerStatus.Active)
            return Result<Unit>.Failure(UserErrors.AlreadyActive); 

        Status = OwnerStatus.Active;
        SetUpdated();

        return Result<Unit>.Success(Unit.Value);
    }
}
