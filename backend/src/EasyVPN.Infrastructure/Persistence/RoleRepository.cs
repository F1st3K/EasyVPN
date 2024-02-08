using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Persistence;

public class RoleRepository : IRoleRepository
{
    private static readonly List<Role> _roles = new();
    
    public IEnumerable<RoleType> GetRolesByUserId(Guid userId)
    {
        return _roles.Where(r => r.UserId == userId)
            .Select(r => r.RoleType);
    }

    public void Add(Role role)
    {
        _roles.Add(role);
    }
}