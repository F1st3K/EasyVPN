using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Commands.DisableConnection;

public record DisableConnectionCommand(Guid ConnectionId) : IRequest<ErrorOr<Updated>>;