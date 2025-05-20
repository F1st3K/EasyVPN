using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    public User? GetByLogin(string login);
    public User? GetById(Guid id);
    public IEnumerable<User> GetAll();
    public void Add(User user);
    public void Update(User user);
}