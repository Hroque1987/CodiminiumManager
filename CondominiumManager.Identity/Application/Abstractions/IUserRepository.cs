using CondominiumManager.Identity.Domain.Entities;

namespace CondominiumManager.Identity.Application.Abstractions;

internal interface IUserRepository : IReadOnlyUserRepository
{
    Task<User> RegisterAsync(User user);  
}
