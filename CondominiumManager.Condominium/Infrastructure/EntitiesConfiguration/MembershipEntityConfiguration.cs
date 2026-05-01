using CondominiumManager.Condominium.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CondominiumManager.Condominium.Infrastructure.EntitiesConfiguration;

internal class MembershipEntityConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.ToTable("Memberships");

        builder.HasKey(member => new { member.UserId, member.BuildingId });

        builder.Property(member => member.Role).HasConversion<string>().IsRequired();

        builder.Property(member => member.UserId).IsRequired();
        builder.Property(member => member.BuildingId).IsRequired();
        builder.Property(member => member.CreatedAt).IsRequired();

        builder.HasOne<Building>()
            .WithMany()
            .HasForeignKey(member => member.BuildingId);
    }
}
