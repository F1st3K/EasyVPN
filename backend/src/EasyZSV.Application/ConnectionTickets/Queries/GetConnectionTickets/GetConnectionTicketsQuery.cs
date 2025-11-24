using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.ConnectionTickets.Queries.GetConnectionTickets;

public record GetConnectionTicketsQuery(
    Guid? ClientId = null
    ) : IRequest<ErrorOr<List<ConnectionTicket>>>;
