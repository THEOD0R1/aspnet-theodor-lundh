using PulseTemple.Domain.Exceptions.Custom;

namespace PulseTemple.Domain.Entities.CustomerService;

public sealed class ContactRequest
{
    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string? PhoneNumber { get; }
    public string Message { get; }
    public DateTime CreatedAt { get; }
    public bool MarkedAsRead { get; private set; }

    private ContactRequest(Guid id, string firstName, string lastName, string email, string? phoneNumber, string message, DateTime createdAt, bool markedAsRead = false)
    {
        if (id == Guid.Empty)
            throw new ValidationDomainException($"Id must be provided");

        Id = id;
        FirstName = Required(firstName, nameof(firstName));
        LastName = Required(lastName, nameof(lastName));
        Email = Required(email, nameof(email));
        PhoneNumber = phoneNumber;
        Message = Required(message, nameof(message));
        CreatedAt = createdAt;
        MarkedAsRead = markedAsRead;
    }
    private static string Required(string? value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValidationDomainException($"{propertyName} must be provided");

        return value.Trim();
    }
    public static ContactRequest Create(string firstName, string lastName, string email, string? phoneNumber, string message)
        => new(Guid.NewGuid(), firstName, lastName, email, phoneNumber, message, DateTime.UtcNow, false);

    public static ContactRequest Rehydrate(Guid id, string firstName, string lastName, string email, string? phoneNumber, string message, DateTime createdAt, bool markedAsRead)
        => new(id, firstName, lastName, email, phoneNumber, message, createdAt, markedAsRead);

    public void MarkAsRead()
    {
        if (MarkedAsRead)
            return;

        MarkedAsRead = true;
    }

    public void MarkAsUnread()
    {
        if (!MarkedAsRead)
            return;

        MarkedAsRead = false;
    }
}
