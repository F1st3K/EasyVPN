using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Servers.Queries.GetServer;

public class GetServerHandler : IRequestHandler<GetServerQuery, ErrorOr<Server>>
{
    private readonly IServerRepository _serverRepository;

    public GetServerHandler(IServerRepository serverRepository)
    {
        _serverRepository = serverRepository;
    }

    public async Task<ErrorOr<Server>> Handle(GetServerQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_serverRepository.Get(query.ServerId) is not { } server)
            return Errors.Server.NotFound;

        return server;
    }
}
