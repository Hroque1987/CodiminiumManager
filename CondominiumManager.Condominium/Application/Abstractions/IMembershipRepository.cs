using CondominiumManager.Condominium.Domain.Entities;

namespace CondominiumManager.Condominium.Application.Abstractions;

internal interface IMembershipRepository : IReadOnlyMembershipRepository
{
    void Add(Membership membership);
}
