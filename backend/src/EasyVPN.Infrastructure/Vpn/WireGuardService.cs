using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Vpn;

public class WireGuardService : IVpnService
{
    public string GetConfig(Guid connectionId)
    {
        return "password=qwertyi1234567";
    }

    public void CreateClient(Connection connection)
    {
        
    }

    public void EnableClient(Guid connectionId)
    {
        throw new NotImplementedException();
    }

    public void DisableClient(Guid connectionId)
    {
        throw new NotImplementedException();
    }

    public void DeleteClient(Guid connectionId)
    {
        throw new NotImplementedException();
    }
}