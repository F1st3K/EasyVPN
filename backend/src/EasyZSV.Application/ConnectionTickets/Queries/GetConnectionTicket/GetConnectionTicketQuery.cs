using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.ConnectionTickets.Queries.GetConnectionTicket;

public record GetConnectionTicketQuery(
    Guid ConnectionTicketId
    ) : IRequest<ErrorOr<ConnectionTicket>>;