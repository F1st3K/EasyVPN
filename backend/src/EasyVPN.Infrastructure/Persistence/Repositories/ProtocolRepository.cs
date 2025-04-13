using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Persistence.Repositories;

public class ProtocolRepository : IProtocolRepository
{
    private readonly EasyVpnDbContext _dbContext;

    public ProtocolRepository(EasyVpnDbContext dbContext)
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
}