using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Commands.AddLifetimeConnection;

public record AddLifetimeConnectionCommand(
    Guid ConnectionId,
    int CountDays) : IRequest<ErrorOr<Success>>;