namespace EasyVPN.Contracts.Connections;

public record ConnectionConfigResponse(
    Guid ClientId,
    string Config
    );