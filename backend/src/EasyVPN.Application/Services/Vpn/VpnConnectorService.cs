using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;

namespace EasyVPN.Application.Services.Vpn;

public class VpnConnectorService
{
    private readonly IUserRepository _userRepository;
    private readonly IServerRepository _serverRepository;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public VpnConnectorService(
        IUserRepository userRepository,
        IServerRepository serverRepository,
        IConnectionRepository connectionRepository,
        IVpnServiceFactory vpnServiceFactory, 
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _serverRepository = serverRepository;
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
        _dateTimeProvider = dateTimeProvider;
    }

    public ErrorOr<Success> CreateConnection(Guid userId, Guid serverId, int countDays)
    {
        if (_userRepository.GetUserById(userId) is not { } user)
            return Errors.User.NotFound;

        if (_serverRepository.Get(serverId) is not { } server)
            return Errors.Server.NotFound;

        var connection = new Connection()
        {
            Id = Guid.NewGuid(),
            ClientId = user.Id,
            ExpirationTime = _dateTimeProvider.UtcNow.AddDays(countDays),
            ServerId = server.Id,
            Status = ConnectionStatus.Pending
        };
        _connectionRepository.Add(connection);
        _vpnServiceFactory.GetVpnService(server)
            .CreateClient(connection);
        
        return new Success();
    }

    public void ExtendConnection()
    {
        
    }

    public void DeleteConnection()
    {
        
    }

    public void ActivateConnection()
    {
        
    }
}