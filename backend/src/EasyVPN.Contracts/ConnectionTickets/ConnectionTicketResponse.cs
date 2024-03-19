namespace EasyVPN.Contracts.ConnectionTickets;

public record ConnectionTicketResponse(
    Guid Id,
    Guid ConnectionId,
    Guid ClientId,
    string Status,
    DateTime CreationTime,
    int Days,
    string PaymentDescription);