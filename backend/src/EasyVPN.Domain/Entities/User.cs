using EasyVPN.Domain.Common.Enums;

namespace EasyVPN.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public IEnumerable<RoleType> Roles { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string HashPassword { get; set; } = null!;
}