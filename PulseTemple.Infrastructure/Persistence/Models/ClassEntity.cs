using PulseTemple.Infrastructure.Persistence.Models;

namespace PulseTemple.Infrastructure.Persistence.Models;

public sealed class ClassEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? InstructorName { get; set; }
    public string? Category { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Capacity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;

    public ICollection<BookingEntity> Bookings { get; set; } = [];
}