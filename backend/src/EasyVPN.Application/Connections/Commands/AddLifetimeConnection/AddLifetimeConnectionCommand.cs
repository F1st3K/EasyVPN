using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Commands.AddLifetimeConnection;

public record AddLifetimeConnectionCommand(
    Guid ConnectionId,
    int CountDays) : IRequest<ErrorOr<Updated>>;