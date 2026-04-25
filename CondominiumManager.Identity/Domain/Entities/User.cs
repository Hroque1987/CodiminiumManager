using CondominiumManager.Identity.Domain.Enums;
using CondominiumManager.Identity.Domain.Errors;
using CondominiumManager.Identity.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Sharedkernel.Results;
using Sharedkernel.Utils;

namespace CondominiumManager.Identity.Domain.Entities;

internal sealed class User : BaseEntity
{
    public FullName Name { get; private set; }
    public Email Email { get; private set; }
    public OwnerStatus Status { get; private set; }

    public string Password { get; private set; }

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

    public static Result<User> Create(FullName name, Email email, string password)
    {
        if (name is null)
            return OwnerErrors.OwnerFullNameEmpty;

        if (email is null)
            return OwnerErrors.OwnerEmailEmpty;
        if (string.IsNullOrWhiteSpace(password))
            return OwnerErrors.EmptyPassword;

        return new User(name, email, password);
    }

    public Result<Unit> ChangeEmail(Email newEmail)
    {
        if (Status == OwnerStatus.Inactive)
            return OwnerErrors.Inactive;


        Email = newEmail;
        SetUpdated();

        return Unit.Value;
    }
    public Result<Unit> Inactivate()
    {
        if (Status == OwnerStatus.Inactive)
            return OwnerErrors.AlreadyInactive;

        Status = OwnerStatus.Inactive;
        SetUpdated();

        return Unit.Value;
    }

    public Result<Unit> Activate()
    {
        if (Status == OwnerStatus.Active)
            return OwnerErrors.AlreadyActive;

        Status = OwnerStatus.Active;
        SetUpdated();

        return Unit.Value;
    }
}
