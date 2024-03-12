using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Persistence;

public class ConnectionTicketRepository : IConnectionTicketRepository
{
    private static readonly List<ConnectionTicket> _connectionTickets = new();
    
    public ConnectionTicket? Get(Guid id)
    {
        return _connectionTickets.SingleOrDefault(c => c.Id == id);
    }

    public IEnumerable<ConnectionTicket> GetAll()
    {
        return _connectionTickets.AsEnumerable();
    }

    public void Add(ConnectionTicket connection)
    {
        _connectionTickets.Add(connection);
    }

    public void Remove(Guid id)
    {
        _connectionTickets.RemoveAll(c => c.Id == id);
    }

    public void Update(ConnectionTicket connection)
    {
        if (_connectionTickets.SingleOrDefault(c => c.Id == connection.Id) is not {} stateConnection)
            return;
        stateConnection.Status = connection.Status;
    }
}