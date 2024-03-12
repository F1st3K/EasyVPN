using ErrorOr;
using MediatR;

namespace EasyVPN.Application.ConnectionTickets.Commands.CreateConnectionTicket;

public record CreateConnectionTicketCommand(
    Guid ConnectionId,
    float Price,
    string Description) : IRequest<ErrorOr<Success>>;