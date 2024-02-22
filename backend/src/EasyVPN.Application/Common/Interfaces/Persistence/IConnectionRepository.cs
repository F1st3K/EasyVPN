using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface IConnectionRepository
{
    public Connection? Get(Guid id);
    public List<Connection> Select();
    public List<Connection> Select(Guid clientId);
    public void Add(Connection connection);
}