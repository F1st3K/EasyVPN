using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Services;
using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Application.Connections.Commands.AddLifetimeConnection;
using Moq;

namespace EasyZsV.Application.UnitTests.Connections.Commands.AddLifetimeConnection;

public class AddLifetimeConnectionMocks
{
    public readonly Mock<IServerRepository> ServerRepository = new();
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IZsvServiceFactory> ZsvServiceFactory = new();
    public readonly Mock<IZsvService> ZsvService = new();
    public readonly Mock<ITaskRepository> TaskRepository = new();

    public AddLifetimeConnectionCommandHandler CreateHandler()
    {

        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.UtcNow)
            .Returns(Constants.Time.Now);
        return new AddLifetimeConnectionCommandHandler(
            ConnectionRepository.Object,
            ZsvServiceFactory.Object,
            mockDateTimeProvider.Object,
            TaskRepository.Object
        );
    }
}