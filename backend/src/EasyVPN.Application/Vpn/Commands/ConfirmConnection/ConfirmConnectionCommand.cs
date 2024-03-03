using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Commands.ConfirmConnection;

public record ConfirmConnectionCommand(
    Guid ConnectionId,
    int CountDays) : IRequest<ErrorOr<Success>>;