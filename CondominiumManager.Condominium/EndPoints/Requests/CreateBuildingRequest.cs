namespace CondominiumManager.Condominium.CondominiumEndPoints.Requests;

internal record CreateBuildingRequest(
    string Name,
    string Street,
    string DoorNumber,
    string PostalCode,
    string City,
    string Country
    );