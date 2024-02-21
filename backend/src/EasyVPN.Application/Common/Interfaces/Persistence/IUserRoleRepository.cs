using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface IUserRoleRepository
{
    public IEnumerable<RoleType> GetRolesByUserId(Guid userId);
    public void Add(UserRole userRole);
}