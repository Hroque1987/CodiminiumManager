using CondominiumManager.Condominium.Domain.Entities;
using CondominiumManager.Condominium.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CondominiumManager.Condominium.Infrastructure.EntitiesConfiguration;

internal class BuildingEntityConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.ToTable("Buildings");

        builder.HasKey(building => building.Id);
        builder.Property(building => building.CreatedAt).IsRequired();
        builder.Property(building => building.Name).IsRequired().HasMaxLength(200);
        builder.OwnsOne(building => building.Address, address =>
        {
            address.Property(a => a.Street).HasColumnName("Street").IsRequired().HasMaxLength(Address.StreetMaxLength);
            address.Property(a => a.DoorNumber).HasColumnName("DoorNumber").IsRequired().HasMaxLength(Address.DoorMaxLength);
            address.Property(a => a.PostalCode).HasColumnName("PostalCode").IsRequired().HasMaxLength(Address.PostalCodeMaxLength);
            address.Property(a => a.City).HasColumnName("City").IsRequired().HasMaxLength(Address.CityMaxLength);
            address.Property(a => a.Country).HasColumnName("Country").IsRequired().HasMaxLength(Address.CountryMaxLength);
        });
        builder.OwnsOne(building => building.Settings, settings =>
        {
            settings.Property(s => s.CurrencyCode).HasColumnName("CurrencyCode");
            settings.Property(s => s.DueDay).HasColumnName("DueDay");
        });
    }
}
