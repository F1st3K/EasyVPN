using EasyZsV.Domain.Entities;

namespace EasyZsV.Application.Common.Interfaces.Persistence;

public interface IProtocolRepository
{
    public Protocol? Get(Guid id);
    public IEnumerable<Protocol> GetAll();
    public void Create(Protocol protocol);
    public void Update(Protocol protocol);
    public void Remove(Guid id);
}