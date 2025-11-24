using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Queries.GetConnections;

public record GetConnectionsQuery(
    Guid? ClientId = null
    ) : IRequest<ErrorOr<List<Connection>>>;