using CondominiumManager.Condominium.Errors;
using Sharedkernel.Errors;
using Sharedkernel.Results;

namespace CondominiumManager.Condominium.Domain.ValueObjects;

internal sealed record Address
{
    public const int StreetMaxLength = 200;
    public const int DoorMaxLength = 6;
    public const int CityMaxLength = 100;
    public const int PostalCodeMaxLength = 20;
    public const int CountryMaxLength = 100;

    public string Street { get; } = default!;
    public string DoorNumber { get; } = default!;
    public string PostalCode{ get; } = default!;
    public string City { get; } = default!;
    public string Country { get; } = default!;

    private Address() { }
    private Address(string street,string doorNumber, string postalCode, string city, string country)
    {
        Street = street;
        DoorNumber = doorNumber;
        PostalCode = postalCode;
        City = city;
        Country = country;
    }

    public static Result<Address> Create(string street, string doorNumber, string postalCode, string city, string country)
    {
        var errors= new List<Error>();

        ValidationHelper(street, StreetMaxLength, CondominiumErrors.AddressErrors.StreetEmpty, CondominiumErrors.AddressErrors.StreetTooLong, errors);
        ValidationHelper(doorNumber, DoorMaxLength, CondominiumErrors.AddressErrors.DoorNumberEmpty, CondominiumErrors.AddressErrors.DoorNumberTooLong, errors);
        ValidationHelper(postalCode, PostalCodeMaxLength, CondominiumErrors.AddressErrors.PostalCodeEmpty, CondominiumErrors.AddressErrors.PostalCodeTooLong, errors);
        ValidationHelper(city, CityMaxLength, CondominiumErrors.AddressErrors.CityEmpty, CondominiumErrors.AddressErrors.CityTooLong, errors);
        ValidationHelper(country, CountryMaxLength, CondominiumErrors.AddressErrors.CountryEmpty, CondominiumErrors.AddressErrors.CountryTooLong, errors);  
        

        if(errors.Count > 0)
            return Result<Address>.Failure(errors);

        return Result<Address>.Success(new Address(street, doorNumber, postalCode, city, country));
    }


    private static void ValidationHelper(string value, int maxLength, Error expectedEmptyError, Error expectedLengthError, List<Error> errors)
    {
        
        if (string.IsNullOrWhiteSpace(value))
            errors.Add(expectedEmptyError);

        if (value.Length > maxLength)
            errors.Add(expectedLengthError);


    }
}
