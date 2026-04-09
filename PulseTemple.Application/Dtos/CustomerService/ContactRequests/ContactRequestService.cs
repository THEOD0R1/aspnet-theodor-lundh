using PulseTemple.Application.Dtos.CustomerService.ContactRequests.Inputs;
using PulseTemple.Application.Dtos.Results;
using PulseTemple.Domain.Abstractions.Repositories;
using PulseTemple.Domain.Entities.CustomerService;
using System.Diagnostics;

namespace PulseTemple.Application.Dtos.CustomerService.ContactRequests;

public sealed class ContactRequestService(IContactRequestRepository repo) : IContactRequestService
{
    public async Task<Result> CreateContactRequestAsync(ContactRequestInput input, CancellationToken ct = default)
    {
        try
        {
            if (input is null)
                return Result.Error("Input model is required");

            var model = ContactRequest.Create(
                input.FirstName,
                input.LastName,
                input.Email,
                input.PhoneNumber,
                input.Message
            );

            await repo.AddAsync(model, ct);
            return Result.Ok();

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);

            return Result.Error();
        }
    }

    public Task<Result> DeleteContactRequestAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ContactRequest?>> GetContactRequestAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IReadOnlyList<ContactRequest>>> GetContactRequestsAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> MarkAsReadAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> MarkAsUnreadAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
