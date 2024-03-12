using EasyVPN.Domain.Common.Enums;

namespace EasyVPN.Domain.Entities;

public class ConnectionTicket
{
    public Guid Id { get; set; }
    public Guid ConnectionId { get; set; }
    public ConnectionTicketStatus Status { get; set; }
    public DateTime CreationTime { get; set; }
    public int Days { get; set; }
    public string PaymentDescription { get; set; } = null!;
}