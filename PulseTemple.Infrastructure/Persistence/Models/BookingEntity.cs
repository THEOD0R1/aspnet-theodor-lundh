using System.ComponentModel.DataAnnotations.Schema;

namespace PulseTemple.Infrastructure.Persistence.Models;

public sealed class BookingEntity
{
    public Guid Id { get; set; }

    public required Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public UserEntity User { get; set; } = null!;

    public Guid ClassId { get; set; }
    [ForeignKey("ClassId")]
    public ClassEntity Class { get; set; } = null!;
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
}