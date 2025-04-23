using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface IServerRepository
{
    public Server? Get(Guid id);
    public IEnumerable<Server> GetAll();
    public void Add(Server server);
    public void Update(Server server);
}
