using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    public User? GetUserByLogin(string login);
    public User? GetUserById(Guid id);
    public void Add(User user);
}