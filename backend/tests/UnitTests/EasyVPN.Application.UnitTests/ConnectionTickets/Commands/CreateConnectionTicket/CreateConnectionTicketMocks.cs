using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.ConnectionTickets.Commands.CreateConnectionTicket;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using Moq;

namespace EasyVPN.Application.UnitTests.ConnectionTickets.Commands.CreateConnectionTicket;

public class CreateConnectionTicketMocks
{
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IConnectionTicketRepository> ConnectionTicketRepository = new();

    public CreateConnectionTicketCommandHandler CreateHandler()
    {
        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.UtcNow)
            .Returns(Constants.Time.Now);
        return new CreateConnectionTicketCommandHandler(
            mockDateTimeProvider.Object,
            ConnectionRepository.Object,
            ConnectionTicketRepository.Object);
    }
}