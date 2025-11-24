using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Entities;

namespace EasyZsV.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly EasyZsvDbContext _dbContext;

    public UserRepository(EasyZsvDbContext dbContext)
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

    public IEnumerable<User> GetAll()
    {
        return _dbContext.Users;
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();
    }
}