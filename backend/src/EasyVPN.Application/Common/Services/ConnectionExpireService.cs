using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Services;

public class ConnectionExpireService
{
    private readonly IExpirationChecker _expirationChecker;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IServerRepository _serverRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;

    public ConnectionExpireService(
        IExpirationChecker expirationChecker,
        IConnectionRepository connectionRepository,
        IServerRepository serverRepository,
        IVpnServiceFactory vpnServiceFactory)
    {
        _expirationChecker = expirationChecker;
        _connectionRepository = connectionRepository;
        _serverRepository = serverRepository;
        _vpnServiceFactory = vpnServiceFactory;
    }

    public void AddActiveConnectionsToTrackExpire()
    {
        _connectionRepository.GetAll().AsParallel()
            .Where(c => c.Status == ConnectionStatus.Active)
            .ForAll(AddTrackExpire);
    }
    
    public void AddTrackExpire(Connection connection)
    {
        _expirationChecker.NewExpire(connection.ExpirationTime,
            () => TryConnectionExpire(connection.Id));
    }

    private bool TryConnectionExpire(Guid connectionId)
    {
        if (_connectionRepository.Get(connectionId) is not {} connection)
            return false;
        
        if (_serverRepository.Get(connection.ServerId) is not { } server)
            return false;
        
        if (_vpnServiceFactory.GetVpnService(server) is not { } vpnService)
            return false;
        
        connection.Status = ConnectionStatus.Expired;
        _connectionRepository.Update(connection);
        vpnService.DisableClient(connection.Id);
        
        return true;
    }
}