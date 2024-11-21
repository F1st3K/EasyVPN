using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Commands.DisableConnection;

public record DisableConnectionCommand(Guid ConnectionId, Server Server) : IRequest<ErrorOr<Updated>>;