using CondominiumManager.Identity.Domain.Enums;
using CondominiumManager.Identity.Domain.Errors;
using CondominiumManager.Identity.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
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
            throw new ArgumentNullException(nameof(name), OwnerErrors.OwnerFullNameEmpty.Message);
           
        if (email is null)
            throw new ArgumentNullException(nameof(name), OwnerErrors.OwnerEmailEmpty.Message);
      
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(name), OwnerErrors.EmptyPassword.Message);

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
