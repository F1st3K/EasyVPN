namespace EasyVPN.Contracts.Servers;

public record ConnectionRequest(
    string Auth,
    string Endpoint);