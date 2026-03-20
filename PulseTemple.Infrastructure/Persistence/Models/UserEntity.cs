
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PulseTemple.Infrastructure.Extensions.Models;

public sealed class UserEntity : IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Guid? MembershipId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;

    [ForeignKey("MembershipId")]
    public MembershipEntity? Membership { get; set; }
    public ICollection<BookingEntity> Bookings { get; set; } = [];
}
