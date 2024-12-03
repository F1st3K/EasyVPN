using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Commands.DisableConnection;

public class DisableConnectionCommandHandler : IRequestHandler<DisableConnectionCommand, ErrorOr<Updated>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;

    public DisableConnectionCommandHandler(IConnectionRepository connectionRepository, IVpnServiceFactory vpnServiceFactory)
    {
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
    }

    public async Task<ErrorOr<Updated>> Handle(DisableConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (_vpnServiceFactory.GetVpnService(connection.Server) is not { } vpnService)
            return Errors.Server.FailedGetService;

        var disableResult = vpnService.DisableClient(command.ConnectionId);
        if (disableResult.IsError)
            return disableResult.ErrorsOrEmptyList;

        return Result.Updated;
    }
}
