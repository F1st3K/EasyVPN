using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Servers.Queries.GetServers;

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
