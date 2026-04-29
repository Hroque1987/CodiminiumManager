using CondominiumManager.Condominium.Domain.Entities;
using CondominiumManager.Condominium.Domain.ValueObjects;
using Sharedkernel.Errors;

namespace CondominiumManager.Condominium.Errors;

internal class CondominiumErrors
{
    internal static class BuildingErrors
    {
        public static readonly Error EmptyName =
            Error.Domain("BUILDING_NAME_EMPTY", "Building name cannot be empty");

        public static readonly Error NameTooLong =
            Error.Domain("BUILDING_NAME_TOO_LONG", $"Building name cannot exceed {Building.NameMaxLength} characters");
    }

    internal static class  AddressErrors
    {
        public static readonly Error StreetEmpty =
            Error.Domain("ADDRESS_STREET_EMPTY", "Street cannot be empty");

        public static readonly Error DoorNumberEmpty =
           Error.Domain("ADDRESS_DOOR_NUMBER_EMPTY", "Door number cannot be empty");

        public static readonly Error PostalCodeEmpty =
           Error.Domain("ADDRESS_POSTAL_CODE_EMPTY", "Postal code cannot be empty");

        public static readonly Error CityEmpty =
           Error.Domain("ADDRESS_CITY_EMPTY", "City cannot be empty");

        public static readonly Error CountryEmpty =
           Error.Domain("ADDRESS_COUNTRY_EMPTY", "Country cannot be empty");

        public static readonly Error StreetTooLong =
           Error.Domain("ADDRESS_STREET_TOO_LONG", $"Street cannot exceed maximum length {Address.StreetMaxLength}");

        public static readonly Error DoorNumberTooLong =
           Error.Domain("ADDRESS_DOOR_NUMBER_TOO_LONG", $"Door number cannot exceed maximum length {Address.DoorMaxLength}");

        public static readonly Error PostalCodeTooLong =
           Error.Domain("ADDRESS_POSTAL_CODE_TOO_LONG", $"Postal code cannot exceed maximum length {Address.PostalCodeMaxLength}");

        public static readonly Error CityTooLong =
           Error.Domain("ADDRESS_CITY_TOO_LONG", $"City cannot exceed maximum length {Address.CityMaxLength}");

        public static readonly Error CountryTooLong =
           Error.Domain("ADDRESS_COUNTRY_TOO_LONG", $"Country cannot exceed maximum length {Address.CountryMaxLength}");

    }

    internal static class BuildingSettingsErrors
    {
        public static readonly Error CurrencyEmpty =
            Error.Domain("EMPTY_CURRENCY", "Currency cannot be empty");

        public static readonly Error DueDayInvalid =
            Error.Domain("INVALID_DUEDAY", "Due day is invalid day");

    }

}