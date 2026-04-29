using CondominiumManager.Condominium.Domain.Entities;

namespace CondominiumManager.Condominium.Application.Abstractions;

internal interface IBuildingRepository : IReadOnlyBuildingRepository
{

    Task<Building> CreateBuildingAsync(Building building);
}
