namespace PulseTemple.Infrastructure.Extensions.Models;

public sealed class MembershipEntity
{
    public Guid Id { get; set; } 
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
}