using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.Common.Service;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Commands.AddLifetimeConnection;

public class AddLifetimeConnectionCommandHandler : IRequestHandler<AddLifetimeConnectionCommand, ErrorOr<Success>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IServerRepository _serverRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IConnectionExpireService _expireService;

    public AddLifetimeConnectionCommandHandler(
        IConnectionRepository connectionRepository, 
        IServerRepository serverRepository,
        IVpnServiceFactory vpnServiceFactory, 
        IDateTimeProvider dateTimeProvider,
        IConnectionExpireService expireService)
    {
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
        _dateTimeProvider = dateTimeProvider;
        _expireService = expireService;
        _serverRepository = serverRepository;
    }
    
    public async Task<ErrorOr<Success>> Handle(AddLifetimeConnectionCommand command, CancellationToken cancellationToken)
    {   
        await Task.CompletedTask;

        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (_serverRepository.Get(connection.ServerId) is not { } server)
            return Errors.Server.NotFound;

        if (_vpnServiceFactory.GetVpnService(server) is not { } vpnService)
            return Errors.Server.FailedGetService;

        if (connection.IsActive == false)
        {
            connection.IsActive = true;
            connection.ExpirationTime = _dateTimeProvider.UtcNow;
        }
        
        connection.ExpirationTime = 
            connection.ExpirationTime.AddDays(command.CountDays);
        _connectionRepository.Update(connection);
        
        vpnService.EnableClient(connection.Id);
        _expireService.AddTrackExpire(connection);
        
        return new Success();
    }
}