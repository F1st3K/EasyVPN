using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface IProtocolRepository
{
    public Protocol? Get(Guid id);
    public IEnumerable<Protocol> GetAll();
}