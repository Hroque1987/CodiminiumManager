using CondominiumManager.Identity.Application.Abstractions;
using CondominiumManager.Identity.Infrastructure.Errors;
using CondominiumManager.Identity.Domain.Entities;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;

    public UserRepository(IdentityDbContext context)
    {
        _context = context;
    }
    public async Task<Result<User>> RegisterAsync(User user)
    {
        try
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return Result<User>.Success(user);
        }
        catch (Exception)
        {
            return Result<User>.Failure(InfrastructureErrors.PersistanceError);
        }

    }

}
