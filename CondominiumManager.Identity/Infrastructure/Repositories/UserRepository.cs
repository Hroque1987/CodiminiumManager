using CondominiumManager.Identity.Application.Abstractions;
using CondominiumManager.Identity.Infrastructure.Errors;
using CondominiumManager.Identity.Domain.Entities;
using Sharedkernel.Results;
using Microsoft.EntityFrameworkCore;

namespace CondominiumManager.Identity.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;

    public UserRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(user => user.Email.Value == email);
     
    }


    public async Task<User> RegisterAsync(User user)
    {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

    }

}


