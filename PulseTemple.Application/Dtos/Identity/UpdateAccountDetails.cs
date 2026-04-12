namespace PulseTemple.Application.Dtos.Identity;

public record UpdateAccountDetails(
    string Email,
    string FirstName,
    string LastName,
    string? PhoneNumber = null,
    string? ImageUrl = null
    );