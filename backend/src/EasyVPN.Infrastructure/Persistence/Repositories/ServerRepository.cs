using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyVPN.Infrastructure.Persistence.Repositories;

public class ServerRepository : IServerRepository
{
    private readonly EasyVpnDbContext _dbContext;

    public ServerRepository(EasyVpnDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Server? Get(Guid id)
    {
        return _dbContext.Servers.Include(s => s.Protocol)
            .SingleOrDefault(s => s.Id == id);
    }

    public IEnumerable<Server> GetAll()
    {
        return _dbContext.Servers.Include(s => s.Protocol);
    }

    public void Add(Server server)
    {
        _dbContext.Servers.Add(server);
        _dbContext.SaveChanges();
    }

    public void Update(Server server)
    {
        if (_dbContext.Servers.SingleOrDefault(s => s.Id == server.Id)
            is not { } stateServer)
            return;
        
        stateServer.Protocol = server.Protocol;
        stateServer.ConnectionString = server.ConnectionString;
        stateServer.Version = server.Version;
        
        _dbContext.Servers.Update(server);
        _dbContext.SaveChanges();
    }
}
