using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Commands.ResetLifetimeConnection;

public record ResetLifetimeConnectionCommand(
    Guid ConnectionId) : IRequest<ErrorOr<Updated>>;