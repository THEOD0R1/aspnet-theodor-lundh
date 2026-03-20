using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseTemple.Infrastructure.Extensions.Models;

namespace PulseTemple.Infrastructure.Extensions.Configurations.Memberships;

public sealed class MembershipEntityConfiguration : IEntityTypeConfiguration<MembershipEntity>
{
    public void Configure(EntityTypeBuilder<MembershipEntity> builder)
    {
        builder.ToTable("Memberships");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(m => m.Name)
           .IsUnique()
           .HasDatabaseName("IX_Membership_Name");

        builder.Property(m => m.Price)
            .HasPrecision(18, 2);

        builder.Property(m => m.Status)
            .HasMaxLength(50);

        builder.Property(m => m.RowVersion)
            .IsRowVersion();
    }
}