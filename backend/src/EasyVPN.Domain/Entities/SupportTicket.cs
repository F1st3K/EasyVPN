using EasyVPN.Domain.Common.Enums;

namespace EasyVPN.Domain.Entities;

public class SupportTicket
{
    public Guid Id { get; set; }
    public SupportTicketStatus Status { get; set; }
    public DateTime CreationTime { get; set; }
    public string ContactDetails { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IEnumerable<string> Images { get; set; } = null!;
}