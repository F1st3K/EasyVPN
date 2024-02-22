using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Queries.GetConnections;

public class GetConnectionsQueryHandler : IRequestHandler<GetConnectionsQuery, ErrorOr<List<Connection>>>
{
    private readonly IConnectionRepository _connectionRepository;

    public GetConnectionsQueryHandler(IConnectionRepository connectionRepository)
    {
        _connectionRepository = connectionRepository;
    }
    
    public async Task<ErrorOr<List<Connection>>> Handle(GetConnectionsQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var connections = _connectionRepository.GetAll();

        if (query.ClientId is { } clientId)
            connections = connections.Where(c => c.ClientId == clientId);
            
        return connections.ToList();
    }
}