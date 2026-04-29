using CondominiumManager.Condominium.Errors;
using Sharedkernel.Errors;
using Sharedkernel.Results;

namespace CondominiumManager.Condominium.Domain.ValueObjects;

internal sealed record BuildingSettings
{
    public string CurrencyCode { get; private set; } = "EUR";
    public int DueDay { get; private set; } = 8;


    private BuildingSettings() { }

    private BuildingSettings(string currencyCode, int dueDay)
    {
        CurrencyCode = currencyCode;
        DueDay = dueDay;
    }

    public static Result<BuildingSettings> Create(string currencyCode, int dueDay)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(currencyCode))
            errors.Add(CondominiumErrors.BuildingSettingsErrors.CurrencyEmpty);

        if (dueDay < 1 || dueDay > 31)
            errors.Add(CondominiumErrors.BuildingSettingsErrors.DueDayInvalid);

        if(errors.Count > 0)
            return Result<BuildingSettings>.Failure(errors);

        return Result<BuildingSettings>.Success(new BuildingSettings(currencyCode, dueDay));
    }

    public static Result<BuildingSettings> Create()
    {
        return Result<BuildingSettings>.Success(new BuildingSettings());
    }

   
}
