using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Persistence;

public class UserRoleRepository : IUserRoleRepository
{
    private static readonly List<UserRole> _userRoles = new()
    {
        new UserRole()
        {
            Id = Guid.NewGuid(), 
            UserId = Guid.Parse("00000001-0000-0000-0000-000000000000"),
            Type = RoleType.Client
        },
        new UserRole()
        {
            Id = Guid.NewGuid(), 
            UserId = Guid.Parse("00000002-0000-0000-0000-000000000000"),
            Type = RoleType.PaymentReviewer
        },
        new UserRole()
        {
            Id = Guid.NewGuid(), 
            UserId = Guid.Parse("00000003-0000-0000-0000-000000000000"),
            Type = RoleType.Administrator
        },
    };
    
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