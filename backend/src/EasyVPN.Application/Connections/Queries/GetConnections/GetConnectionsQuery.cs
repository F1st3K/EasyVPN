using EasyVPN.Application.Connections.Queries.GetConfig;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Queries.GetConnections;

public record GetConnectionsQuery(
    Guid? ClientId = null
    ) : IRequest<ErrorOr<List<Connection>>>;