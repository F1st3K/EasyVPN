using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    public User? GetUserByLogin(string login);
    public void Add(User user);
}