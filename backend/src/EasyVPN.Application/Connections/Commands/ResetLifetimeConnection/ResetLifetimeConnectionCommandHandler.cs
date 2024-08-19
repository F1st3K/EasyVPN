using EasyVPN.Application.Common.Interfaces.Expire;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Commands.ResetLifetimeConnection;

public class ResetLifetimeConnectionCommandHandler : IRequestHandler<ResetLifetimeConnectionCommand, ErrorOr<Updated>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IExpireService<Connection> _expireService;

    public ResetLifetimeConnectionCommandHandler(
        IConnectionRepository connectionRepository,
        IServerRepository serverRepository,
        IVpnServiceFactory vpnServiceFactory,
        IDateTimeProvider dateTimeProvider,
        IExpireService<Connection> expireService)
    {
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
        _dateTimeProvider = dateTimeProvider;
        _expireService = expireService;
    }

    public async Task<ErrorOr<Updated>> Handle(ResetLifetimeConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (_vpnServiceFactory.GetVpnService(connection.Server) is not { } vpnService)
            return Errors.Server.FailedGetService;

        connection.ExpirationTime = _dateTimeProvider.UtcNow;

        _expireService.ResetTrackExpire(connection);
        _connectionRepository.Update(connection);

        var disableResult = vpnService.DisableClient(connection.Id);
        if (disableResult.IsError)
            return disableResult.ErrorsOrEmptyList;

        return Result.Updated;
    }
}
