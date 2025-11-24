using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Queries.GetConnection;

public record GetConnectionQuery(
    Guid ConnectionId
    ) : IRequest<ErrorOr<Connection>>;