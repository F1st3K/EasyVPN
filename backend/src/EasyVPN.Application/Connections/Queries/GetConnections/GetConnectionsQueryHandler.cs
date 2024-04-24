using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Queries.GetConnections;

public class GetConnectionsQueryHandler : IRequestHandler<GetConnectionsQuery, ErrorOr<List<Connection>>>
{
    private readonly IConnectionRepository _connectionRepository;

    public GetConnectionsQueryHandler(
        IConnectionRepository connectionRepository)
    {
        _connectionRepository = connectionRepository;
    }
    
    public async Task<ErrorOr<List<Connection>>> Handle(GetConnectionsQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var connections = _connectionRepository.GetAll();

        var list = new List<Connection>();
        foreach (var connection in connections)
        {
            if (query.ClientId is { } clientId
                && connection.Client.Id != clientId)
                continue;
            list.Add(connection);
            Console.WriteLine(list.Last().Client);
        }
        
            
        
        return list;
    }
}