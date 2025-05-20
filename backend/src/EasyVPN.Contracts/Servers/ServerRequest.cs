namespace EasyVPN.Contracts.Servers;

public record ServerRequest(
    Guid ProtocolId,
    ConnectionRequest Connection,
    string Version);