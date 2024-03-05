using EasyVPN.Application.Common.Interfaces.Expire;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;

namespace EasyVPN.Application.Expire;

public class ConnectionExpireService : IExpireService<Connection>
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

    public void AddAllToTrackExpire()
    {
        _connectionRepository.GetAll().AsParallel()
            .Where(c => c.IsActive)
            .ForAll(AddTrackExpire);
    }
    
    public void AddTrackExpire(Connection connection)
    {
        _expirationChecker.TryRemoveExpire(connection.Id);
        _expirationChecker.NewExpire(connection.Id,
            connection.ExpirationTime,
            () => TryConnectionExpire(connection.Id));
    }

    private ErrorOr<Success> TryConnectionExpire(Guid connectionId)
    {
        if (_connectionRepository.Get(connectionId) is not { } connection)
            return Errors.Connection.NotFound;
        
        if (_serverRepository.Get(connection.ServerId) is not { } server)
            return Errors.Server.NotFound;
        
        if (_vpnServiceFactory.GetVpnService(server) is not { } vpnService)
            return Errors.Server.FailedGetService;
        
        connection.IsActive = false;
        _connectionRepository.Update(connection);
        vpnService.DisableClient(connection.Id);
        
        return new Success();
    }
}