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
        _dbContext.Connections.Remove(
            _dbContext.Connections.Single(ct => ct.Id == id));
        _dbContext.SaveChanges();
    }

    public void Update(Connection connection)
    {
        _dbContext.Connections.Update(connection);
        _dbContext.SaveChanges();
    }
}