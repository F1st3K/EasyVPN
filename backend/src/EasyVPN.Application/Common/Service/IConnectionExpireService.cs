using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Service;

public interface IConnectionExpireService
{
    public void AddActiveConnectionsToTrackExpire();
    public void AddTrackExpire(Connection connection);
}