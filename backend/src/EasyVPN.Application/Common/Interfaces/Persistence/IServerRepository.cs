using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface IServerRepository
{
    public Server? Get(Guid id);
}