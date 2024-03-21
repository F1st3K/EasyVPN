using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

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
        return _dbContext.Servers.SingleOrDefault(s => s.Id == id);
    }
}