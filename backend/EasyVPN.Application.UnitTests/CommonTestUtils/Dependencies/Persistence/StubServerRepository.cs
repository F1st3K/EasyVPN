using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.CommonTestUtils.Dependencies;

public class StubServerRepository : IServerRepository
{
    private readonly List<Server> _servers = new()
    {
        new Server { Host = "localhost:54034", Id = Guid.Empty, Type = VpnType.WireGuard}
    };
    
    public Server? Get(Guid id)
    {
        return _servers.SingleOrDefault(s => s.Id == id);
    }
}