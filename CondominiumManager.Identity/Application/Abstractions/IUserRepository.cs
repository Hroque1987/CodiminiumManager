using CondominiumManager.Identity.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.Application.Abstractions;

internal interface IUserRepository : IReadOnlyUserRepository
{
    Task<Result<User>> RegisterAsync(User user);  
}
