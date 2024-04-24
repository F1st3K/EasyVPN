using EasyVPN.Contracts.Users;

namespace EasyVPN.Contracts.Connections;

public record ConnectionResponse(
    Guid Id,
    UserResponse Client,
    Guid ServerId,
    DateTime ValidUntil);