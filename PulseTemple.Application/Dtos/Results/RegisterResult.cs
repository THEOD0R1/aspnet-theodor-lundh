namespace PulseTemple.Application.Dtos.Results;

public record RegisterResult
    (
        bool Succeded,
        IReadOnlyCollection<string> Errors,
        Guid? UserId = null
    );
