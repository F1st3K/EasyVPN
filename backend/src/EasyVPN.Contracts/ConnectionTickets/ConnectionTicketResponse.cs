using EasyZsV.Contracts.Users;

namespace EasyZsV.Contracts.ConnectionTickets;

public record ConnectionTicketResponse(
    Guid Id,
    Guid ConnectionId,
    UserResponse Client,
    string Status,
    DateTime CreationTime,
    int Days,
    string Description,
    string[] Images);
