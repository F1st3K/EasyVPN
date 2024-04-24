namespace EasyVPN.Contracts.Connections;

public record CreateConnectionRequest(
    Guid ServerId,
    int Days,
    string Description,
    string[] Images
    );