using System.Collections;
using EasyZsV.Domain.Entities;

namespace EasyZsV.Application.Common.Interfaces.Persistence;

public interface IConnectionRepository
{
    public Connection? Get(Guid id);
    public IEnumerable<Connection> GetAll();
    public void Add(Connection connection);
    public void Remove(Guid id);
    public void Update(Connection connection);
}