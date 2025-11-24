using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Services;
using EasyZsV.Application.ConnectionTickets.Commands.CreateConnectionTicket;
using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using Moq;

namespace EasyZsV.Application.UnitTests.ConnectionTickets.Commands.CreateConnectionTicket;

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