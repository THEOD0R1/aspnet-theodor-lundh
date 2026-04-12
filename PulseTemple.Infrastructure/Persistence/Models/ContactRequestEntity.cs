namespace PulseTemple.Infrastructure.Persistence.Models;

public sealed class ContactRequestEntity
{
    public Guid Id { get; set; } 
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public required string Message { get; set; }
    public bool MarkedAsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}