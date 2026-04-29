using CondominiumManager.Condominium.Application.Abstractions;
using CondominiumManager.Condominium.Application.Contracts.Commands;
using CondominiumManager.Condominium.Domain.Entities;
using CondominiumManager.Condominium.Domain.ValueObjects;
using Sharedkernel.Abstractions;
using Sharedkernel.Results;

namespace CondominiumManager.Condominium.Application.Usecases;

internal class CreateBuildingHandler : IUseCaseHandler<CreateBuildingCommand, Guid>
{
    readonly IBuildingRepository _buildingRepository;

    public CreateBuildingHandler(IBuildingRepository buildingRepository)
    {
        _buildingRepository = buildingRepository;
    }

    public async Task<Result<Guid>> HandleAsync(CreateBuildingCommand createBuildingCommand, CancellationToken ct)
    {
        var adressResult = Address.Create(createBuildingCommand.Street, 
                                    createBuildingCommand.DoorNumber, 
                                    createBuildingCommand.PostalCode, 
                                    createBuildingCommand.City, 
                                    createBuildingCommand.Country);


        var buildingSettingsResult = BuildingSettings.Create();

        if(adressResult.IsFailure || buildingSettingsResult.IsFailure)
            return Result<Guid>.Failure([..adressResult.Errors, ..buildingSettingsResult.Errors]);

        var newBuildingResult = Building.Create(createBuildingCommand.Name, adressResult.Value, buildingSettingsResult.Value);

        if(newBuildingResult.IsFailure)
            return Result<Guid>.Failure([.. newBuildingResult.Errors]);

        var savedBuilding = await  _buildingRepository.CreateBuildingAsync(newBuildingResult.Value);

        return Result<Guid>.Success(savedBuilding.Id);
    }
}
