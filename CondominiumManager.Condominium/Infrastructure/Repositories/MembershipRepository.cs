using CondominiumManager.Condominium.Application.Abstractions;
using CondominiumManager.Condominium.Domain.Entities;

namespace CondominiumManager.Condominium.Infrastructure.Repositories;

internal class MembershipRepository : IMembershipRepository
{
    private readonly CondominiumDbContext _context;

    public MembershipRepository(CondominiumDbContext context)
    {
        _context = context;
    }

    public void Add(Membership membership)
    {
        _context.Memberships.Add(membership);
    }

}
