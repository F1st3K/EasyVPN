using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface IConnectionRepository
{
    public Connection? Get(Guid id);
    public IEnumerable<Connection> GetAll();
    public void Add(Connection connection);
    public void Update(Connection connection);
}