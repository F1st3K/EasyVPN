using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new()
    {
        new User(){ Id = Guid.Parse("00000001-0000-0000-0000-000000000000"), FirstName = "FreakClient", LastName = "Fister"},
        new User(){ Id = Guid.Parse("00000002-0000-0000-0000-000000000000"), FirstName = "FreakPayment", LastName = "Fister"},
        new User(){ Id = Guid.Parse("00000003-0000-0000-0000-000000000000"), FirstName = "FreakAdmin", LastName = "Fister"}
    };

    private readonly EasyVpnDbContext _dbContext;

    public UserRepository(EasyVpnDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User? GetUserByLogin(string login)
    {
        return _dbContext.Users.SingleOrDefault(user => user.Login == login);
    }

    public User? GetUserById(Guid id)
    {
        // TODO: remove this later
        if (_users.SingleOrDefault(user => user.Id == id) is { } su)
            return su;
        return _dbContext.Users.SingleOrDefault(user => user.Id == id);
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }
}