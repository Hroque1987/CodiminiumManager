using CondominiumManager.Identity.Domain.Entities;
using CondominiumManager.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CondominiumManager.Identity.Infrastructure;

internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(owner => owner.Id);

        builder.Property(owner => owner.CreatedAt).IsRequired();

        // Email (Value Object -> string)
        builder.Property(owner => owner.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Value
            )
            .IsRequired();

        // FullName (simple version: string)
        builder.OwnsOne(owner => owner.Name, name =>
        {
            name.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .IsRequired();

            name.Property(x => x.LastName)
                .HasColumnName("LastName")
                .IsRequired();
        });

        builder.Property(owner => owner.Status).IsRequired();



    }
}
