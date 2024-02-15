namespace EasyVPN.Application.Vpn.Queries.GetConfig;

public record GetConfigResult(
    Guid ClientId,
    string Config
    );