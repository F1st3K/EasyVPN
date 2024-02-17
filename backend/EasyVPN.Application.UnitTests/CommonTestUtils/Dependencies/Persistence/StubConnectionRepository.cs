using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.CommonTestUtils.Dependencies;

public class StubConnectionRepository : IConnectionRepository
{
    private readonly List<Connection> _connections = new();
    
    public Connection? Get(Guid id)
    {
        return _connections.SingleOrDefault(c => c.Id == id);
    }

    public void Add(Connection connection)
    {
        _connections.Add(connection);
    }
}