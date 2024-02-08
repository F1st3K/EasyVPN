namespace EasyVPN.Domain.Entities;

public class Role
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public RoleType RoleType { get; set; }
}