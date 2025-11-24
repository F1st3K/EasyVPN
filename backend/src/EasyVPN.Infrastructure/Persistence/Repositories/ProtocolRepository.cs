using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Entities;

namespace EasyZsV.Infrastructure.Persistence.Repositories;

public class ProtocolRepository : IProtocolRepository
{
    private readonly EasyZsvDbContext _dbContext;

    public ProtocolRepository(EasyZsvDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Protocol? Get(Guid id)
    {
        return _dbContext.Protocols.SingleOrDefault(p => p.Id == id);
    }

    public IEnumerable<Protocol> GetAll()
    {
        return _dbContext.Protocols;
    }

    public void Create(Protocol protocol)
    {
        _dbContext.Protocols.Add(protocol);
        _dbContext.SaveChanges();
    }

    public void Update(Protocol protocol)
    {
        _dbContext.Protocols.Update(protocol);
        _dbContext.SaveChanges();
    }

    public void Remove(Guid id)
    {
        _dbContext.Protocols.Remove(
            _dbContext.Protocols.Single(p => p.Id == id));
        _dbContext.SaveChanges();
    }
}