using System;
using System.Collections.Generic;
using System.Text;

namespace CondominiumManager.Condominium.Domain.ValueObjects;

internal record BuildingAddress
{
    public string Street { get; private set; } = default!;
    public string DoorNumber { get; set; } = default!;
    public string PostalCode{ get; set; } = default!;
    public string City { get; private set; } = default!;
    public string Country { get; private set; } = default!;

    private BuildingAddress() { }
    private BuildingAddress(string street,string doorNumber, string postalCode, string city, string country)
    {
        Street = street;
        DoorNumber = doorNumber;
        PostalCode = postalCode;
        City = city;
        Country = country;
    }

    public static BuildingAddress Create(string street, string doorNumber, string postalCode, string city, string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentNullException(nameof(street), "Street cannot be empty.");

        if (string.IsNullOrWhiteSpace(doorNumber))
            throw new ArgumentNullException(nameof(doorNumber), "Door number cannot be empty.");

        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentNullException(nameof(postalCode), "Postal code cannot be empty.");

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentNullException(nameof(city), "City cannot be empty.");

        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentNullException(nameof(country), "Country cannot be empty.");

        return new BuildingAddress(street, doorNumber, postalCode, city, country);
    }
       
    

   // public override string ToString() => $"{FirstName} {LastName}";
}
