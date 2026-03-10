using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseTemple.Infrastructure.Persistence.Models;

namespace PulseTemple.Infrastructure.Persistence.Configurations.Users;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");

        builder.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(e => e.LastName).HasMaxLength(100).IsRequired();
        
        builder.Property(e => e.MembershipId).IsRequired(false);

        builder.Property(e => e.RowVersion).IsRowVersion();

        builder.HasOne(u => u.Membership)
       .WithMany()
       .HasForeignKey(u => u.MembershipId)
       .OnDelete(DeleteBehavior.SetNull);
    }
}

