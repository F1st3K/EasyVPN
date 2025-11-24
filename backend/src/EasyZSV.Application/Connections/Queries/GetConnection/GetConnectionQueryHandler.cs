using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Queries.GetConnection;

public class GetConnectionQueryHandler : IRequestHandler<GetConnectionQuery, ErrorOr<Connection>>
{
    private readonly IConnectionRepository _connectionRepository;

    public GetConnectionQueryHandler(IConnectionRepository connectionRepository)
    {
        _connectionRepository = connectionRepository;
    }

    public async Task<ErrorOr<Connection>> Handle(GetConnectionQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(query.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        return connection;
    }
}