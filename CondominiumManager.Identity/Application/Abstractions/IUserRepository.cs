using CondominiumManager.Identity.Domain.Entities;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.Application.Abstractions;

internal interface IUserRepository : IReadOnlyUserRepository
{
    Task<User> RegisterAsync(User user);  
}
