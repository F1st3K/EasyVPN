using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Vpn;

public interface IVpnService
{
    public string GetConfig(Guid connectionId);
    public void CreateClient(Guid connectionId);
    public void EnableClient(Guid connectionId);
    public void DisableClient(Guid connectionId);
    public void DeleteClient(Guid connectionId);
}