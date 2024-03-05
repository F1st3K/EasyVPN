using EasyVPN.Application.Common.Interfaces.Expire;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Commands.ResetLifetimeConnection;

public class ResetLifetimeConnectionCommandHandler : IRequestHandler<ResetLifetimeConnectionCommand, ErrorOr<Success>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IServerRepository _serverRepository;
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
        _serverRepository = serverRepository;
    }
    
    public async Task<ErrorOr<Success>> Handle(ResetLifetimeConnectionCommand command, CancellationToken cancellationToken)
    {   
        await Task.CompletedTask;

        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (_serverRepository.Get(connection.ServerId) is not { } server)
            return Errors.Server.NotFound;

        if (_vpnServiceFactory.GetVpnService(server) is not { } vpnService)
            return Errors.Server.FailedGetService;

        connection.ExpirationTime = _dateTimeProvider.UtcNow;
        
        _expireService.ResetTrackExpire(connection);
        _connectionRepository.Update(connection);
        
        vpnService.DisableClient(connection.Id);
        
        return new Success();
    }
}