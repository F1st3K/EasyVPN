using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Commands.DeleteConnection;

public record DeleteConnectionCommand(
    Guid ConnectionId) : IRequest<ErrorOr<Success>>;