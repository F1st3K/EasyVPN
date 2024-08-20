using EasyVPN.Application.Common.Interfaces.Expire;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Commands.AddLifetimeConnection;

public class AddLifetimeConnectionCommandHandler : IRequestHandler<AddLifetimeConnectionCommand, ErrorOr<Updated>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IExpireService<Connection> _expireService;

    public AddLifetimeConnectionCommandHandler(
        IConnectionRepository connectionRepository,
        IVpnServiceFactory vpnServiceFactory,
        IDateTimeProvider dateTimeProvider,
        IExpireService<Connection> expireService)
    {
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
        _dateTimeProvider = dateTimeProvider;
        _expireService = expireService;
    }

    public async Task<ErrorOr<Updated>> Handle(AddLifetimeConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (_vpnServiceFactory.GetVpnService(connection.Server) is not { } vpnService)
            return Errors.Server.FailedGetService;

        if (connection.ExpirationTime < _dateTimeProvider.UtcNow)
            connection.ExpirationTime = _dateTimeProvider.UtcNow;

        connection.ExpirationTime =
            connection.ExpirationTime.AddDays(command.CountDays);
        _connectionRepository.Update(connection);

        var enableResult = vpnService.EnableClient(connection.Id);
        if (enableResult.IsError)
            return enableResult.ErrorsOrEmptyList;

        _expireService.ResetTrackExpire(connection);
        _expireService.AddTrackExpire(connection);

        return Result.Updated;
    }
}
