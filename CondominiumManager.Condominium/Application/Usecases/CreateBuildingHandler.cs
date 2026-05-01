using CondominiumManager.Condominium.Application.Abstractions;
using CondominiumManager.Condominium.Application.Contracts.Commands;
using CondominiumManager.Condominium.Domain.Entities;
using CondominiumManager.Condominium.Domain.ValueObjects;
using Sharedkernel.Abstractions;
using Sharedkernel.Results;

namespace CondominiumManager.Condominium.Application.Usecases;

internal class CreateBuildingHandler : IUseCaseHandler<CreateBuildingCommand, Guid>
{
    private readonly IBuildingRepository _buildingRepository;
    private readonly IUnitOfWork _unitOfWork;


    public CreateBuildingHandler(IBuildingRepository buildingRepository , IUnitOfWork unitOfWork)
    {
        _buildingRepository = buildingRepository;
        _unitOfWork = unitOfWork;
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

        _buildingRepository.Add(newBuildingResult.Value);


        await _unitOfWork.CommitAsync(ct);

        return Result<Guid>.Success(newBuildingResult.Value.Id);
    }
}
