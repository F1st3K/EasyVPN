namespace EasyVPN.Contracts.Connections;

public record ExtendConnectionRequest(
    Guid ConnectionId,
    int Days,
    string Description,
    string[] Images
    );