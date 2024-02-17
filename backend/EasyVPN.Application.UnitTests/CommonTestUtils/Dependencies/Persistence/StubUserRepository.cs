using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.CommonTestUtils.Dependencies;

public class StubUserRepository : IUserRepository
{
    private readonly List<User> _users = new ();
    
    public User? GetUserByLogin(string login)
    {
        return _users.SingleOrDefault(user => user.Login == login);
    }

    public User? GetUserById(Guid id)
    {
        return _users.SingleOrDefault(user => user.Id == id);
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
}