namespace IP.Shared.IntegrationEvents;

public record CreatedUserIntegrationEvent(
    Guid Id,
    string User,
    string Email,
    Guid RoleId,
    string RoleName,
    string TokenHash) : IIntegrationEvent;
