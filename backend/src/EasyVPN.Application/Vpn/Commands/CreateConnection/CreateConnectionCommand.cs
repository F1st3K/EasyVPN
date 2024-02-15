using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Commands.CreateConnection;

public record CreateConnectionCommand(
    Guid ClientId,
    Guid ServerId,
    int CountDays) : IRequest<ErrorOr<Success>>;