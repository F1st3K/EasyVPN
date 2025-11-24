namespace EasyZsV.Contracts.Servers;

public record ServerResponse(
    Guid Id,
    ProtocolResponse Protocol,
    string Version);