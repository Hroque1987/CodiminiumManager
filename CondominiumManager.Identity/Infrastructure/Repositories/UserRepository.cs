using CondominiumManager.Identity.Application.Abstractions;
using CondominiumManager.Identity.Domain.Entities;
using CondominiumManager.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CondominiumManager.Identity.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;

    public UserRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByEmailAsync(Email email)
    {
        return await _context.Users.AnyAsync(user => user.Email == email);
     
    }

    public async Task<User?> GetUserByEmail(Email email)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User> RegisterAsync(User user)
    {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

    }

}


