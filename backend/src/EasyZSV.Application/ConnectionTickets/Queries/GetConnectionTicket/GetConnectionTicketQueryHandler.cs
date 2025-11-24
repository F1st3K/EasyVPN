using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.ConnectionTickets.Queries.GetConnectionTicket;

public class GetConnectionTicketQueryHandler : IRequestHandler<GetConnectionTicketQuery, ErrorOr<ConnectionTicket>>
{
    private readonly IConnectionTicketRepository _connectionTicketRepository;

    public GetConnectionTicketQueryHandler(IConnectionTicketRepository connectionTicketRepository)
    {
        _connectionTicketRepository = connectionTicketRepository;
    }

    public async Task<ErrorOr<ConnectionTicket>> Handle(GetConnectionTicketQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionTicketRepository.Get(query.ConnectionTicketId) is not { } ticket)
            return Errors.ConnectionTicket.NotFound;

        return ticket;
    }
}