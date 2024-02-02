using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();
    
    public User? GetUserByLogin(string login)
    {
        return _users.SingleOrDefault(user => user.Login == login);
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
}