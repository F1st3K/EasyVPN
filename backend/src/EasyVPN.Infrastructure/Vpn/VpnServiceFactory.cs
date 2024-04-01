using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Vpn;

public class VpnServiceFactory : IVpnServiceFactory
{
    public IVpnService? GetVpnService(Server server)
    {
        return server.Type switch
        {
            VpnType.WireGuard => WireGuardService.GetService(server.ConnectionString),
            _ => throw new NotSupportedException(
                $"Unsupported {nameof(VpnType)}: {server.Type.ToString()}")
        };
    }
}