using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Services;
using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Application.Connections.Commands.ResetLifetimeConnection;
using MediatR;
using Moq;

namespace EasyZsV.Application.UnitTests.Connections.Commands.ResetLifetimeConnection;

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