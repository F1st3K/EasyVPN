using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Persistence;

public class ConnectionRepository : IConnectionRepository
{
    private static readonly List<Connection> _connections = new();
    
    public Connection? Get(Guid id)
    {
        return _connections.SingleOrDefault(c => c.Id == id);
    }

    public List<Connection> Select()
    {
        return _connections.ToList();
    }

    public List<Connection> Select(Guid clientId)
    {
        return _connections.Where(c => c.ClientId == clientId).ToList();
    }

    public void Add(Connection connection)
    {
        _connections.Add(connection);
    }
}