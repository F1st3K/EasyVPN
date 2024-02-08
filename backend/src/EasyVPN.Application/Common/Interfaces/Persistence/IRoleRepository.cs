using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface IRoleRepository
{
    public IEnumerable<RoleType> GetRolesByUserId(Guid userId);
    public void Add(Role role);
}