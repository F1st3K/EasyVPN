using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new()
    {
        new User(){ Id = Guid.Parse("00000001-0000-0000-0000-000000000000"), Roles = new [] { RoleType.Client }, FirstName = "FreakClient", LastName = "Fister"},
        new User(){ Id = Guid.Parse("00000002-0000-0000-0000-000000000000"), Roles = new [] { RoleType.PaymentReviewer }, FirstName = "FreakPayment", LastName = "Fister"},
        new User(){ Id = Guid.Parse("00000003-0000-0000-0000-000000000000"), Roles = new [] { RoleType.Administrator }, FirstName = "FreakAdmin", LastName = "Fister"}
    };

    private readonly EasyVpnDbContext _dbContext;

    public UserRepository(EasyVpnDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User? GetByLogin(string login)
    {
        return _dbContext.Users.SingleOrDefault(user => user.Login == login);
    }

    public User? GetById(Guid id)
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