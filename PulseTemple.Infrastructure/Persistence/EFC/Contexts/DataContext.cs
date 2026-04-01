using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PulseTemple.Infrastructure.Persistence.Models;

namespace PulseTemple.Infrastructure.Persistence.EFC.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity, RoleEntity, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
    public DbSet<MembershipEntity> Memberships { get; set; }
    public DbSet<ClassEntity> Classes { get; set; }
    public DbSet<BookingEntity> Bookings { get; set; }
}
