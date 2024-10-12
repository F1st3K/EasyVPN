using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTickets;

public record GetConnectionTicketsQuery(
    Guid? ClientId = null
    ) : IRequest<ErrorOr<List<ConnectionTicket>>>;
