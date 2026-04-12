namespace PulseTemple.Application.Abstractions.Users;

public record UpdateUserRequest
(
    Guid Id,
    string? FirstName = null,
    string? LastName = null,
    string? PhoneNumber = null
);