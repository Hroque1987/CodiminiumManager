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

        builder.HasKey(user => user.Id);

        builder.Property(user => user.CreatedAt).IsRequired();

        builder.Property(user => user.Password).IsRequired();

        // Email (Value Object -> string)
        builder.Property(user => user.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value)
            )
            .IsRequired();
        builder.HasIndex(user => user.Email).IsUnique();

        // FullName (simple version: string)
        builder.OwnsOne(user => user.Name, name =>
        {
            name.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .IsRequired().HasMaxLength(FullName.MaxFirstNameLength);

            name.Property(x => x.LastName)
                .HasColumnName("LastName")
                .IsRequired().HasMaxLength(FullName.MaxLastNameLength);
        });

        builder.Property(user => user.Status).IsRequired();



    }
}
