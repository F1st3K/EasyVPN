using ErrorOr;
using MediatR;

namespace EasyZsV.Application.ConnectionTickets.Commands.RejectConnectionTicket;

public record RejectConnectionTicketCommand(Guid Id) : IRequest<ErrorOr<Success>>;