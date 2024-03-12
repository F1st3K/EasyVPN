using ErrorOr;
using MediatR;

namespace EasyVPN.Application.ConnectionTickets.Commands.RejectConnectionTicket;

public record RejectConnectionTicketCommand(Guid Id) : IRequest<ErrorOr<Success>>;