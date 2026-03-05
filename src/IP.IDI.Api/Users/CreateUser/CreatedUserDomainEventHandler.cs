using IP.Shared.IntegrationEvents;

namespace IP.IDI.Api.Users.CreateUser;

public record CreatedUserDomainEvent(
    Guid Id,
    string User,
    string Email,
    Guid RoleId,
    string RoleName,
    string TokenHash) : IDomainEvent;

internal class CreatedUserDomainEventHandler(IEventBus _eventBus) : 
    IDomainEventHandler<CreatedUserDomainEvent>
{
    public async Task Handle(
        CreatedUserDomainEvent domainEvent, 
        CancellationToken cancellationToken)
    {
        await _eventBus.Publish(
            new CreatedUserIntegrationEvent(
                domainEvent.Id, 
                domainEvent.User, 
                domainEvent.Email, 
                domainEvent.RoleId, 
                domainEvent.RoleName,
                domainEvent.TokenHash));

       await Task.CompletedTask;
    }
}