namespace EasyVPN.Domain.Entities;

public class Connection
{
    public Guid Id { get; set; }
    public User Client { get; set; } = null!;
    public Guid ServerId { get; set; }
    public DateTime ExpirationTime { get; set; }
}