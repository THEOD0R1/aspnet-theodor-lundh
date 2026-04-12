namespace PulseTemple.Application.Abstractions.Users;

public record UserDetails
(
    Guid Id,
    Guid? MembershipId,
    string? FirstName,
    string? LastName,
    string? ImageUrl,
    string? Email,
    string? PhoneNumber,
    MembershipDetails? Membership = null
);
