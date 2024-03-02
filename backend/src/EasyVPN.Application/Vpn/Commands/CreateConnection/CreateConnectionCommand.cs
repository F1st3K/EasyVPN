using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Commands.CreateConnection;

public record CreateConnectionCommand(
    Guid ClientId,
    Guid ServerId) : IRequest<ErrorOr<Success>>;