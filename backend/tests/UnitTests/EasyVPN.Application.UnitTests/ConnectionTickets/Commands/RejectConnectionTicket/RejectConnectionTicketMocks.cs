using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.ConnectionTickets.Commands.ConfirmConnectionTicket;
using Moq;

namespace EasyVPN.Application.UnitTests.ConnectionTickets.Commands.RejectConnectionTicket;

public class RejectConnectionTicketMocks
{
    public readonly Mock<IConnectionTicketRepository> ConnectionTicketRepository = new();

    public ConfirmConnectionTicketCommandHandler CreateHandler()
    {
        return new ConfirmConnectionTicketCommandHandler(
            ConnectionTicketRepository.Object);
    }
}