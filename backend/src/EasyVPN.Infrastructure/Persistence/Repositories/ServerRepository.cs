using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Persistence.Repositories;

public class ServerRepository : IServerRepository
{
    private static readonly List<Server> _servers = new()
    {
        new Server { ConnectionString = "localhost:54034", Id = Guid.Empty, Type = VpnType.WireGuard}
    };
    
    public Server? Get(Guid id)
    {
        return _servers.SingleOrDefault(s => s.Id == id);
    }
}