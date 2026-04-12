namespace PulseTemple.Application.Abstractions.Users;

public record UserResult
(
    bool Succeeded,
    IReadOnlyCollection<string> Errors,
    Guid? Id = null
);