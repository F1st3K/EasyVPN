using EasyVPN.Contracts.Users;

namespace EasyVPN.Contracts.ConnectionTickets;

public record ConnectionTicketResponse(
    Guid Id,
    Guid ConnectionId,
    UserResponse Client,
    string Status,
    DateTime CreationTime,
    int Days,
    string PaymentDescription,
    string[] Images);