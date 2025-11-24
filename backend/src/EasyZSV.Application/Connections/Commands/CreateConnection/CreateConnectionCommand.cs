using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Commands.CreateConnection;

public record CreateConnectionCommand(
    Guid ClientId,
    Guid ServerId) : IRequest<ErrorOr<Connection>>;