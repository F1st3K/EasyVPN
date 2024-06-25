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
    private readonly IVpnServiceFactory _vpnServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ConnectionExpireService(
        IExpirationChecker expirationChecker,
        IConnectionRepository connectionRepository,
        IVpnServiceFactory vpnServiceFactory, 
        IDateTimeProvider dateTimeProvider)
    {
        _expirationChecker = expirationChecker;
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
        _dateTimeProvider = dateTimeProvider;
    }

    public void AddAllToTrackExpire()
    {
        _connectionRepository.GetAll().AsParallel()
            .ForAll(ResetTrackExpire);
        _connectionRepository.GetAll().AsParallel()
            .Where(c => c.ExpirationTime > _dateTimeProvider.UtcNow)
            .ForAll(AddTrackExpire);
    }
    
    public void AddTrackExpire(Connection connection)
    {
        _expirationChecker.NewExpire(connection.Id,
            connection.ExpirationTime,
            () => TryConnectionExpire(connection.Id));
    }

    public void ResetTrackExpire(Connection connection)
    {
        _expirationChecker.TryRemoveExpire(connection.Id);
    }

    private ErrorOr<Success> TryConnectionExpire(Guid connectionId)
    {
        if (_connectionRepository.Get(connectionId) is not { } connection)
            return Errors.Connection.NotFound;
        
        if (_vpnServiceFactory.GetVpnService(connection.Server) is not { } vpnService)
            return Errors.Server.FailedGetService;
        
        vpnService.DisableClient(connection.Id);
        
        return Result.Success;
    }
}