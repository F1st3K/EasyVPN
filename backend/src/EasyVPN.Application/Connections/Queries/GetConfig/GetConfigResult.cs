namespace EasyVPN.Application.Connections.Queries.GetConfig;

public record GetConfigResult(
    Guid ClientId,
    string Config
    );