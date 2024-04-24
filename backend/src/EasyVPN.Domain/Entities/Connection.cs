namespace EasyVPN.Domain.Entities;

public class Connection
{
    public Guid Id { get; set; }
    public User Client { get; set; } = null!;
    public Server Server { get; set; } = null!;
    public DateTime ExpirationTime { get; set; }
}