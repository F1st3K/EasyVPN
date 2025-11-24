using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.ConnectionTickets.Commands.ConfirmConnectionTicket;
using Moq;

namespace EasyZsV.Application.UnitTests.ConnectionTickets.Commands.ConfirmConnectionTicket;

public class ConfirmConnectionTicketMocks
{
    public readonly Mock<IConnectionTicketRepository> ConnectionTicketRepository = new();

    public ConfirmConnectionTicketCommandHandler CreateHandler()
    {
        return new ConfirmConnectionTicketCommandHandler(
            ConnectionTicketRepository.Object);
    }
}