namespace EasyVPN.Contracts.Servers;

public record ServerResponse(
    Guid Id,
    ProtocolResponse ProtocolResponse,
    string Version);