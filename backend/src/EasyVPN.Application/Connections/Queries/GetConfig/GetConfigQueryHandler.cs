using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Queries.GetConfig;

public class GetConfigQueryHandler : IRequestHandler<GetConfigQuery, ErrorOr<GetConfigResult>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;

    public GetConfigQueryHandler(
        IConnectionRepository connectionRepository,
        IVpnServiceFactory vpnServiceFactory)
    {
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
    }

    public async Task<ErrorOr<GetConfigResult>> Handle(GetConfigQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(query.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (_vpnServiceFactory.GetVpnService(connection.Server) is not { } vpnService)
            return Errors.Server.FailedGetService;

        var configResult = vpnService.GetConfig(query.ConnectionId);
        if (configResult.IsError)
            return configResult.ErrorsOrEmptyList;

        return new GetConfigResult(connection.Client.Id, configResult.Value);
    }
}
