using ErrorOr;
using MediatR;

namespace EasyVPN.Application.ConnectionTickets.Commands.CreateConnectionTicket;

public record CreateConnectionTicketCommand(
    Guid ConnectionId,
    int Days,
    string Description,
    IEnumerable<string> Images) : IRequest<ErrorOr<Created>>;