using PulseTemple.Application.Dtos.CustomerService.ContactRequests.Inputs;
using PulseTemple.Application.Dtos.Results;
using PulseTemple.Domain.Entities.CustomerService;

namespace PulseTemple.Application.Dtos.CustomerService.ContactRequests;

public interface IContactRequestService
{
    Task<Result> CreateContactRequestAsync(ContactRequestInput input, CancellationToken ct = default);
    Task<Result> MarkAsReadAsync(Guid id, CancellationToken ct = default);
    Task<Result> MarkAsUnreadAsync(Guid id, CancellationToken ct = default);
    Task<Result> DeleteContactRequestAsync(Guid id, CancellationToken ct = default);

    Task<Result<ContactRequest?>> GetContactRequestAsync(Guid id, CancellationToken ct = default);
    Task<Result<IReadOnlyList<ContactRequest>>> GetContactRequestsAsync(Guid id, CancellationToken ct = default);
}
