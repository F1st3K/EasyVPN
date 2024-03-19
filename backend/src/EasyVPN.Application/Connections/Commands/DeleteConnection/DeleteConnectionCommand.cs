using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Commands.DeleteConnection;

public record DeleteConnectionCommand(
    Guid ConnectionId) : IRequest<ErrorOr<Deleted>>;