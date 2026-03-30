namespace PulseTemple.Application.Abstractions.Users;

public record RegisterUserRequest
(
    string Email,
    string Password,

    string? FirstName = null,
    string? LastName = null,
    string? PhoneNumber = null
);