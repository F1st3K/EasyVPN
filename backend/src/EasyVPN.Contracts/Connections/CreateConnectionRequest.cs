namespace EasyVPN.Contracts.Connections;

public record CreateConnectionRequest(
    Guid ServerId,
    float Price,
    string Description
    );