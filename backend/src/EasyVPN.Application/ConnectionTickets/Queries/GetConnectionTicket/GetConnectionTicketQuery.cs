using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTicket;

public record GetConnectionTicketQuery(
    Guid ConnectionTicketId
    ) : IRequest<ErrorOr<ConnectionTicket>>;