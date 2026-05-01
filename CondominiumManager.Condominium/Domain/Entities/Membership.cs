using CondominiumManager.Condominium.Domain.Enums;
using CondominiumManager.Condominium.Errors;
using Sharedkernel.Errors;
using Sharedkernel.Results;


namespace CondominiumManager.Condominium.Domain.Entities;

internal sealed class Membership
{
    public Guid UserId { get; private set; } = default!;
    public Guid BuildingId { get; private set; } = default!;

    public Role Role { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private Membership(){}

    private Membership(Guid userId, Guid buildingId, Role role)
    {
        UserId = userId;
        BuildingId = buildingId;
        Role = role;
    }

    public static Result<Membership> Create(Guid userId, Guid buildingId, Role role)
    {
        var errors = new List<Error>();

        if (userId == Guid.Empty)
            errors.Add(CondominiumErrors.MembershipErrors.UserIdEmpty);


        if (buildingId == Guid.Empty)
            errors.Add(CondominiumErrors.MembershipErrors.BuildingIdEmpty);

        if (!Enum.IsDefined(typeof(Role), role))
            errors.Add(CondominiumErrors.MembershipErrors.RoleInvalid);

        if (errors.Count > 0)
            return Result<Membership>.Failure(errors);

        return Result<Membership>.Success(new Membership(userId, buildingId, role));
    }

    public static Result<Membership> CreateOwner(Guid userId, Guid buildingId)
    {
        return Create(userId, buildingId, Role.Owner);
    }

    public static Result<Membership> CreateAdmin(Guid userId, Guid buildingId)
    {
        return Create(userId, buildingId, Role.Admin);
    }





}
