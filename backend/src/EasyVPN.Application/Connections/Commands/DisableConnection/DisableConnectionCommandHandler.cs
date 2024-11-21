using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Commands.DisableConnection;

public class DisableConnectionCommandHandler : IRequestHandler<DisableConnectionCommand, ErrorOr<Updated>>
{
    private readonly IVpnServiceFactory _vpnServiceFactory;

    public DisableConnectionCommandHandler( IVpnServiceFactory vpnServiceFactory)
    {
        _vpnServiceFactory = vpnServiceFactory;
    }

    public async Task<ErrorOr<Updated>> Handle(DisableConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_vpnServiceFactory.GetVpnService(command.Server) is not { } vpnService)
            return Errors.Server.FailedGetService;

        var disableResult = vpnService.DisableClient(command.ConnectionId);
        if (disableResult.IsError)
            return disableResult.ErrorsOrEmptyList;

        return Result.Updated;
    }
}
