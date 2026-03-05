using IP.Shared.IntegrationEvents;

namespace IP.IDI.Api.Users.RequestChangePassword;

public record RequestedChangePasswordDomainEvent(
    Guid Id,
    string User,
    string Email,
    string TokenHash,
    bool RequestedByAdm) : IDomainEvent;

internal class RequestedChangePasswordDomainEventHandler(IEventBus _eventBus) :
    IDomainEventHandler<RequestedChangePasswordDomainEvent>
{
    public async Task Handle(
        RequestedChangePasswordDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        await _eventBus.Publish(
            new RequestedChangePasswordIntegrationEvent(
                domainEvent.Id,
                domainEvent.User,
                domainEvent.Email,
                domainEvent.TokenHash,
                domainEvent.RequestedByAdm));

        await Task.CompletedTask;
    }
}
