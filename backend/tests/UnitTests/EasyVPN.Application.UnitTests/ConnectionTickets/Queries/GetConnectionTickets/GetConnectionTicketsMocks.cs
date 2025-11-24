using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.ConnectionTickets.Queries.GetConnectionTickets;
using Moq;

namespace EasyZsV.Application.UnitTests.ConnectionTickets.Queries.GetConnectionTickets;

public class GetConnectionTicketsMocks
{
    public readonly Mock<IConnectionTicketRepository> ConnectionTicketRepository = new();

    public GetConnectionTicketsQueryHandler CreateHandler()
    {
        return new GetConnectionTicketsQueryHandler(
            ConnectionTicketRepository.Object);
    }
}