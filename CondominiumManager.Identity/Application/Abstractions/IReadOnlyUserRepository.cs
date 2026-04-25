namespace CondominiumManager.Identity.Application.Abstractions;

internal interface IReadOnlyUserRepository
{
    Task<bool> ExistsByEmailAsync(string email);
}
