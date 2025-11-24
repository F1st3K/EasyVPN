using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Commands.DeleteConnection;

public record DeleteConnectionCommand(
    Guid ConnectionId) : IRequest<ErrorOr<Deleted>>;