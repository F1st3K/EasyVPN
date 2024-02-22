using EasyVPN.Application.Vpn.Queries.GetConfig;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Queries.GetConnections;

public record GetConnectionsQuery(
    Guid? ClientId = null
    ) : IRequest<ErrorOr<List<Connection>>>;