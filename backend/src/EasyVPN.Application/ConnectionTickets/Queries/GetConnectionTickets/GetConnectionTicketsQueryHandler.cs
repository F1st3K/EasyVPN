using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTickets;

public class GetConnectionTicketsQueryHandler : IRequestHandler<GetConnectionTicketsQuery, ErrorOr<List<ConnectionTicket>>>
{
    private readonly IConnectionTicketRepository _connectionTicketRepository;

    public GetConnectionTicketsQueryHandler(IConnectionTicketRepository connectionTicketRepository)
    {
        _connectionTicketRepository = connectionTicketRepository;
    }
    
    public async Task<ErrorOr<List<ConnectionTicket>>> Handle(GetConnectionTicketsQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var connectionTickets = _connectionTicketRepository.GetAll();

        if (query.ClientId is { } clientId)
            connectionTickets = connectionTickets.Where(c => c.Client.Id == clientId);
            
        return connectionTickets.ToList();
    }
}