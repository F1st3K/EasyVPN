using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface IConnectionTicketRepository
{
    public ConnectionTicket? Get(Guid id);
    public IEnumerable<ConnectionTicket> GetAll();
    public void Add(ConnectionTicket connection);
    public void Remove(Guid id);
    public void Update(ConnectionTicket connection);
}