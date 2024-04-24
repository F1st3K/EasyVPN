using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Queries.GetConfig;

public class GetConfigQueryHandler : IRequestHandler<GetConfigQuery, ErrorOr<GetConfigResult>>
{
    private readonly IServerRepository _serverRepository;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;

    public GetConfigQueryHandler(
        IServerRepository serverRepository,
        IConnectionRepository connectionRepository,
        IVpnServiceFactory vpnServiceFactory)
    {
        _serverRepository = serverRepository;
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
    }
    
    public async Task<ErrorOr<GetConfigResult>> Handle(GetConfigQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(query.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (_serverRepository.Get(connection.ServerId) is not { } server)
            return Errors.Server.NotFound;

        if (_vpnServiceFactory.GetVpnService(server) is not { } vpnService)
            return Errors.Server.FailedGetService;

        var config = vpnService.GetConfig(query.ConnectionId);
        return new GetConfigResult(connection.Client.Id, config);
    }
}