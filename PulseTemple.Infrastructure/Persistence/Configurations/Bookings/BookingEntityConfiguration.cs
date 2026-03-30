using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseTemple.Infrastructure.Persistence.Models;

namespace PulseTemple.Infrastructure.Persistence.Configurations;

public sealed class BookingEntityConfiguration : IEntityTypeConfiguration<BookingEntity>
{
    public void Configure(EntityTypeBuilder<BookingEntity> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.Id);

        builder.HasIndex(b => new { b.UserId, b.ClassId })
            .IsUnique()
            .HasDatabaseName("UX_User_Class_Booking");

        builder.HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(b => b.Class)
            .WithMany(c => c.Bookings)
            .HasForeignKey(b => b.ClassId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.Property(b => b.RowVersion)
            .IsRowVersion();
    }
}