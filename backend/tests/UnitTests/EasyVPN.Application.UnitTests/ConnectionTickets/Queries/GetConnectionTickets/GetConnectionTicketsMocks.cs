using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTickets;
using Moq;

namespace EasyVPN.Application.UnitTests.ConnectionTickets.Queries.GetConnectionTickets;

public class GetConnectionTicketsMocks
{
    public readonly Mock<IConnectionTicketRepository> ConnectionTicketRepository = new();
    
    public GetConnectionTicketsQueryHandler CreateHandler()
    {
        return new GetConnectionTicketsQueryHandler(
            ConnectionTicketRepository.Object);
    }
}