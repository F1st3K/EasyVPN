using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Vpn;

public interface IVpnServiceFactory
{
    public IVpnService GetVpnService(Server server);
}