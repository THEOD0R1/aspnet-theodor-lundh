using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseTemple.Infrastructure.Persistence.Models;

namespace PulseTemple.Infrastructure.Persistence.Configurations.Classes;

public sealed class ClassEntityConfiguration : IEntityTypeConfiguration<ClassEntity>
{
    public void Configure(EntityTypeBuilder<ClassEntity> builder)
    {
        builder.ToTable("Classes", t =>
        {
            t.HasCheckConstraint("CK_Class_Duration", "[EndTime] > [StartTime]");
            t.HasCheckConstraint("CK_Class_Capacity_Positive", "[Capacity] > 0");
        });

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.InstructorName)
            .HasMaxLength(100);

        builder.Property(c => c.Category)
            .HasMaxLength(50);

        builder.HasIndex(c => c.StartTime)
            .HasDatabaseName("IX_Class_StartTime");

        builder.HasIndex(c => c.Category)
            .HasDatabaseName("IX_Class_Category");

        builder.Property(c => c.RowVersion)
            .IsRowVersion();
    }
}