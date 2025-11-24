using EasyZsV.Contracts.Servers;
using EasyZsV.Contracts.Users;

namespace EasyZsV.Contracts.Connections;

public record ConnectionResponse(
    Guid Id,
    UserResponse Client,
    ServerResponse Server,
    DateTime ValidUntil);