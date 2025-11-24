namespace EasyZsV.Contracts.Connections;

public record ConnectionConfigResponse(
    Guid ClientId,
    string Config
    );