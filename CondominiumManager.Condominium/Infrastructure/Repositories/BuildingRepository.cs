using CondominiumManager.Condominium.Application.Abstractions;
using CondominiumManager.Condominium.Domain.Entities;

namespace CondominiumManager.Condominium.Infrastructure.Repositories;

internal class BuildingRepository : IBuildingRepository
{
    internal CondominiumDbContext _context;

    public BuildingRepository(CondominiumDbContext context)
    {
        _context = context;

    }
    public async Task<Building> CreateBuildingAsync(Building building)
    {
        await _context.Buildings.AddAsync(building);
        await _context.SaveChangesAsync();
        return building;

    }
}
