using CondominiumManager.Condominium.Domain.ValueObjects;
using CondominiumManager.Condominium.Errors;
using Sharedkernel.Errors;
using Sharedkernel.Results;

namespace CondominiumManager.Condominium.Domain.Entities;

internal class Building : BaseEntity
{
    public const int NameMaxLength = 500;
    public string Name { get; private set; } = default!;
    public Address Address { get; private set; } = default!;

    public BuildingSettings Settings { get; private set; } = default!;

    private Building() { }

    private Building(string name, Address buildingAddress, BuildingSettings settings)
    {
        Name = name;
        Address = buildingAddress;
        Settings = settings;
    }

    public static Result<Building> Create(string name, Address buildingAddress, BuildingSettings settings)
    {
        ArgumentNullException.ThrowIfNull(buildingAddress);

        ArgumentNullException.ThrowIfNull(settings);

        var errors = ValidateName(name);

        if(errors.Count > 0)
            return Result<Building>.Failure(errors);


        return Result<Building>.Success(new Building(name, buildingAddress, settings));
    }

    private static List<Error> ValidateName(string name)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add(CondominiumErrors.BuildingErrors.EmptyName);
            return errors;
        }
      
        if (name.Length > NameMaxLength)
            errors.Add(CondominiumErrors.BuildingErrors.NameTooLong);        
            
        return errors;
    }

}
