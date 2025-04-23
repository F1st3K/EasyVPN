using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyVPN.Infrastructure.Persistence.Repositories;

public class ConnectionTicketRepository : IConnectionTicketRepository
{
    private readonly EasyVpnDbContext _dbContext;

    public ConnectionTicketRepository(EasyVpnDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ConnectionTicket? Get(Guid id)
    {
        return _dbContext.ConnectionTickets
            .Include(ct => ct.Client)
            .SingleOrDefault(c => c.Id == id);
    }

    public IEnumerable<ConnectionTicket> GetAll()
    {
        return _dbContext.ConnectionTickets
            .Include(ct => ct.Client);
    }

    public void Add(ConnectionTicket connection)
    {
        _dbContext.ConnectionTickets.Add(connection);
        _dbContext.SaveChanges();
    }

    public void Remove(Guid id)
    {
        _dbContext.ConnectionTickets.Remove(
            _dbContext.ConnectionTickets.Single(ct => ct.Id == id));
        _dbContext.SaveChanges();
    }

    public void Update(ConnectionTicket connection)
    {
        _dbContext.ConnectionTickets.Update(connection);
        _dbContext.SaveChanges();
    }
}