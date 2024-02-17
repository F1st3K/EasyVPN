using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.CommonTestUtils.Dependencies;

public class StubUserRoleRepository : IUserRoleRepository
{
    private readonly List<UserRole> _userRoles = new();
    
    public IEnumerable<RoleType> GetRolesByUserId(Guid userId)
    {
        return _userRoles.Where(r => r.UserId == userId)
            .Select(r => r.Type);
    }

    public void Add(UserRole userRole)
    {
        _userRoles.Add(userRole);
    }
}