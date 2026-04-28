using CondominiumManager.Identity.Domain.Entities;
using CondominiumManager.Identity.Domain.ValueObjects;

namespace CondominiumManager.Identity.Application.Abstractions;

internal interface IReadOnlyUserRepository
{
    Task<bool> ExistsByEmailAsync(Email email);

    Task<User?> GetUserByEmail(Email email);
}
