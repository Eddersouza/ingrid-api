namespace IP.Shared.IntegrationEvents;

public record RequestedChangePasswordIntegrationEvent(
    Guid Id,
    string User,
    string Email,
    string TokenHash,
    bool RequestedByAdm) : IIntegrationEvent;
