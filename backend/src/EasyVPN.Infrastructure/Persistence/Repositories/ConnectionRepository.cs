using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyVPN.Infrastructure.Persistence.Repositories;

public class ConnectionRepository : IConnectionRepository
{
    private readonly EasyVpnDbContext _dbContext;

    public ConnectionRepository(EasyVpnDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Connection? Get(Guid id)
    {
        return _dbContext.Connections
            .Include(c => c.Client)
            .Include(c => c.Server)
                .ThenInclude(s => s.Protocol)
            .SingleOrDefault(c => c.Id == id);
    }

    public IEnumerable<Connection> GetAll()
    {
        return _dbContext.Connections
            .Include(c => c.Client)
            .Include(c => c.Server)
                .ThenInclude(s => s.Protocol);
    }

    public void Add(Connection connection)
    {
        _dbContext.Connections.Add(connection);
        _dbContext.SaveChanges();
    }

    public void Remove(Guid id)
    {
        if (_dbContext.Connections.SingleOrDefault(ct => ct.Id == id)
            is not { } connection)
            return;

        _dbContext.Connections.Remove(connection);
        _dbContext.SaveChanges();
    }

    public void Update(Connection connection)
    {
        if (_dbContext.Connections.SingleOrDefault(c => c.Id == connection.Id)
            is not { } stateConnection)
            return;

        stateConnection.ExpirationTime = connection.ExpirationTime;
        _dbContext.Connections.Update(stateConnection);
        _dbContext.SaveChanges();
    }
}