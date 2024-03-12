using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Commands.ResetLifetimeConnection;

public record ResetLifetimeConnectionCommand(
    Guid ConnectionId) : IRequest<ErrorOr<Success>>;