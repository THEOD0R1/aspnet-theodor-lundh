using PulseTemple.Domain.Enums;

namespace PulseTemple.Application.Abstractions.Users;

public record MembershipDetails
(
   Guid Id,
   string Name,
   decimal Price,
   MembershipStatus Status
);
