namespace EasyZsV.Contracts.Servers;

public record ConnectionRequest(
    string Auth,
    string Endpoint);