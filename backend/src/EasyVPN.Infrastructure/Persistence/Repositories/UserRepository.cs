using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
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
        return _dbContext.Users.SingleOrDefault(user => user.Id == id);
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }
}