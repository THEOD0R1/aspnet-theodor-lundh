namespace PulseTemple.Application.Abstractions.Users;

public record UserDetails
(
    Guid Id,
    Guid? MembershipId,
    string? FirstName,
    string? LastName,
    string? Email,
    string? PhoneNumber,
    MembershipDetails? Membership = null
);
