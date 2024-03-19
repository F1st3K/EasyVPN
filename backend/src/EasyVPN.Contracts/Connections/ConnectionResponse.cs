namespace EasyVPN.Contracts.Connections;

public record ConnectionResponse(
    Guid Id,
    Guid ClientId,
    Guid ServerId,
    DateTime ValidUntil);