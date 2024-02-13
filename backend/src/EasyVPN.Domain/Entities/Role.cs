using EasyVPN.Domain.Common.Enums;

namespace EasyVPN.Domain.Entities;

public class Role
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public RoleType Type { get; set; }
}