using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Commands.AddLifetimeConnection;

public record AddLifetimeConnectionCommand(
    Guid ConnectionId,
    int CountDays) : IRequest<ErrorOr<Updated>>;