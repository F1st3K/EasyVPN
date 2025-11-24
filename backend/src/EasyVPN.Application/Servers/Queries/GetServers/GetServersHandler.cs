using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Servers.Queries.GetServers;

public class GetServersHandler : IRequestHandler<GetServersQuery, ErrorOr<List<Server>>>
{
    private readonly IServerRepository _serverRepository;

    public GetServersHandler(IServerRepository serverRepository)
    {
        _serverRepository = serverRepository;
    }

    public async Task<ErrorOr<List<Server>>> Handle(GetServersQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return _serverRepository.GetAll().ToList();
    }
}
