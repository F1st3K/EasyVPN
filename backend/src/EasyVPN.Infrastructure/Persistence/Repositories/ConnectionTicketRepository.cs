using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;

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
        return _dbContext.ConnectionTickets.SingleOrDefault(c => c.Id == id);
    }

    public IEnumerable<ConnectionTicket> GetAll()
    {
        return _dbContext.ConnectionTickets;
    }

    public void Add(ConnectionTicket connection)
    {
        _dbContext.ConnectionTickets.Add(connection);
        _dbContext.SaveChanges();
    }

    public void Remove(Guid id)
    {
        if (_dbContext.ConnectionTickets.SingleOrDefault(ct => ct.Id == id)
            is not {} connectionTicket)
            return;
        
        _dbContext.ConnectionTickets.Remove(connectionTicket);
        _dbContext.SaveChanges();
    }

    public void Update(ConnectionTicket connection)
    {
        if (_dbContext.ConnectionTickets.SingleOrDefault(c => c.Id == connection.Id)
            is not {} stateConnection)
            return;
        
        stateConnection.Status = connection.Status;
        _dbContext.ConnectionTickets.Update(stateConnection);
        _dbContext.SaveChanges();
    }
}