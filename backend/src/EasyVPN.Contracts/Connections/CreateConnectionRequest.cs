namespace EasyVPN.Contracts.Connections;

public record CreateConnectionRequest(
    Guid ServerId,
    int CountDays
    );