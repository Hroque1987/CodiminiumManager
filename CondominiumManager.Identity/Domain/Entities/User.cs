using CondominiumManager.Identity.Domain.Enums;
using CondominiumManager.Identity.Domain.Errors;
using CondominiumManager.Identity.Domain.ValueObjects;
using Sharedkernel.Results;
using Sharedkernel.Utils;

namespace CondominiumManager.Identity.Domain.Entities;

internal sealed class User : BaseEntity
{
    public FullName Name { get; private set; }
    public Email Email { get; private set; }
    public OwnerStatus Status { get; private set; }

    private User() { }

    private User(FullName name, Email email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Status = OwnerStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public static Result<User> Create(FullName name, Email email)
    {
        if (name is null)
            return OwnerErrors.OwnerFullNameEmpty;

        if (email is null)
            return OwnerErrors.OwnerEmailEmpty;

        return new User(name, email);
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
