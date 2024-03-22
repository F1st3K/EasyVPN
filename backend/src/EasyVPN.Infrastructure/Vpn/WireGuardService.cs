using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Vpn;

public class WireGuardService : IVpnService
{
    public static WireGuardService GetService(string connectionString)
    {
        return new WireGuardService();
    }
    
    private WireGuardService()
    { }
    
    public string GetConfig(Guid connectionId)
    {
        return "password=qwertyi1234567";
    }

    public void CreateClient(Connection connection)
    {
        
    }

    public void EnableClient(Guid connectionId)
    {
        
    }

    public void DisableClient(Guid connectionId)
    {
        
    }

    public void DeleteClient(Guid connectionId)
    {
        
    }
}