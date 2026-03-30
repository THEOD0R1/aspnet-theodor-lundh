namespace PulseTemple.Application.Dtos;

public record RegisterResult
    (
        bool Succeded,
        IReadOnlyCollection<string> Errors,
        Guid? UserId = null
    );
