using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Connections.Commands.ResetLifetimeConnection;
using MediatR;
using Moq;

namespace EasyVPN.Application.UnitTests.Connections.Commands.ResetLifetimeConnection;

public class ResetLifetimeConnectionMocks
{
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<ITaskRepository> TaskRepository = new();
    public readonly Mock<ISender> Sender = new();

    public ResetLifetimeConnectionCommandHandler CreateHandler()
    {

        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.UtcNow)
            .Returns(Constants.Time.Now);
        return new ResetLifetimeConnectionCommandHandler(
            ConnectionRepository.Object,
            mockDateTimeProvider.Object,
            TaskRepository.Object,
            Sender.Object
        );
    }
}