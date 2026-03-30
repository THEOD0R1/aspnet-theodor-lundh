namespace PulseTemple.Application.Abstractions.Users;

public record UserDetailsResult
(
    bool Succeeded,
    IReadOnlyCollection<string> Errors,
    UserDetails? UserDetails = null
);