using ErrorOr;
using MediatR;

namespace EasyVPN.Application.ConnectionTickets.Commands.ConfirmConnectionTicket;

public record ConfirmConnectionTicketCommand(Guid Id) : IRequest<ErrorOr<Success>>;