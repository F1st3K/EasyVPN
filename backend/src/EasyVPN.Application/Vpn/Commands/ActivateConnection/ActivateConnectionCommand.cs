using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Commands.ActivateConnection;

public record ActivateConnectionCommand(
    Guid ConnectionId,
    int CountDays) : IRequest<ErrorOr<Success>>;