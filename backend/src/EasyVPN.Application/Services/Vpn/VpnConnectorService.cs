using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Vpn;

namespace EasyVPN.Application.Services.Vpn;

public class VpnConnectorService
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IServerRepository _serverRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;

    public VpnConnectorService(
        IConnectionRepository connectionRepository,
        IServerRepository serverRepository,
        IVpnServiceFactory vpnServiceFactory)
    {
        _connectionRepository = connectionRepository;
        _serverRepository = serverRepository;
        _vpnServiceFactory = vpnServiceFactory;
    }

    public void CreateConnection()
    {
        
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