using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Commands.ResetLifetimeConnection;

public record ResetLifetimeConnectionCommand(
    Guid ConnectionId) : IRequest<ErrorOr<Success>>;