using ErrorOr;
using MediatR;

namespace EasyZsV.Application.ConnectionTickets.Commands.ConfirmConnectionTicket;

public record ConfirmConnectionTicketCommand(Guid Id) : IRequest<ErrorOr<Success>>;