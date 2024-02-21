using EasyVPN.Domain.Common.Enums;

namespace EasyVPN.Domain.Entities;

public class SupportTicket
{
    public Guid Id { get; set; }
    public SupportTicketStatus Status { get; set; }
    public string Type { get; set; } = null!;
    public DateTime CreationTime { get; set; }
    public string ContactDetails { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Description { get; set; } = null!;
}