using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Vpn;

public class VpnServiceFactory : IVpnServiceFactory
{
    public IVpnService? GetVpnService(Server server)
    {
        return server.Version switch
        {
            VpnVersion.HttpV1 => HttpV1.GetService(server.ConnectionString),
            _ => throw new NotSupportedException(
                $"Unsupported {nameof(VpnVersion)}: {server.Version.ToString()}")
        };
    }
}