using PulseTemple.Domain.Abstractions.Repositories;
using PulseTemple.Domain.Entities.CustomerService;
using PulseTemple.Infrastructure.Persistence.EFC.Contexts;
using PulseTemple.Infrastructure.Persistence.Models;

namespace PulseTemple.Infrastructure.Persistence.EFC.Repositories.CustomerService;

public sealed class ContactRequestRepository(DataContext context) : RepositoryBase<ContactRequest, Guid, ContactRequestEntity, DataContext>(context), IContactRequestRepository
{
    protected override void ApplyUpdates(ContactRequest model, ContactRequestEntity entity)
    {
        entity.MarkedAsRead = model.MarkedAsRead;
    }

    protected override Guid GetId(ContactRequest model)
    {
        return model.Id;
    }

    protected override ContactRequestEntity ToEntity(ContactRequest model)
    {
        var entity = new ContactRequestEntity()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Message = model.Message,
            MarkedAsRead = model.MarkedAsRead
        };

        return entity;
    }

    protected override ContactRequest ToModel(ContactRequestEntity entity)
    {
        var model = ContactRequest.Rehydrate
            (
             entity.Id,
             entity.FirstName,
             entity.LastName,
             entity.Email,
             entity.PhoneNumber,
             entity.Message,
             entity.CreatedAt,
             entity.MarkedAsRead
            );

        return model;
    }
}
