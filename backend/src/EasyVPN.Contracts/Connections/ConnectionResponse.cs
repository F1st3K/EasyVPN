using EasyVPN.Contracts.Servers;
using EasyVPN.Contracts.Users;

namespace EasyVPN.Contracts.Connections;

public record ConnectionResponse(
    Guid Id,
    UserResponse Client,
    ServerResponse Server,
    DateTime ValidUntil);