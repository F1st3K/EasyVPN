using EasyVPN.Domain.Common.Enums;

namespace EasyVPN.Domain.Entities;

public class Connection
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid ServerId { get; set; }
    public ConnectionStatus Status { get; set; }
    public DateTime ExpirationTime { get; set; }
}