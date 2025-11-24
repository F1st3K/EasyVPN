using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyZsV.Infrastructure.Persistence.Repositories;

public class ServerRepository : IServerRepository
{
    private readonly EasyZsvDbContext _dbContext;

    public ServerRepository(EasyZsvDbContext dbContext)
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
        _dbContext.Servers.Update(server);
        _dbContext.SaveChanges();
    }

    public void Remove(Guid id)
    {
        _dbContext.Servers.Remove(
            _dbContext.Servers.Single(s => s.Id == id));
        _dbContext.SaveChanges();
    }
}
