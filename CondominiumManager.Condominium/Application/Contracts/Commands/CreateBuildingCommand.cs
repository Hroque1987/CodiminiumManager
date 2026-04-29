namespace CondominiumManager.Condominium.Application.Contracts.Commands;

internal record CreateBuildingCommand(
    string Name,
    string Street,
    string DoorNumber,
    string PostalCode,
    string City,
    string Country
    );