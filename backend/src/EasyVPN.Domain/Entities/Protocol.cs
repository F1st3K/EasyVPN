namespace EasyVPN.Domain.Entities;

public class Protocol
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Icon { get; set; } = null!;
}