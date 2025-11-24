using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Services;
using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Application.Connections.Commands.CreateConnection;
using EasyZsV.Application.Connections.Commands.DeleteConnection;
using EasyZsV.Domain.Entities;
using Moq;

namespace EasyZsV.Application.UnitTests.Connections.Commands.DeleteConnection;

public class DeleteConnectionMocks
{
    public readonly Mock<IServerRepository> ServerRepository = new();
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IZsvServiceFactory> ZsvServiceFactory = new();
    public readonly Mock<IZsvService> ZsvService = new();

    public DeleteConnectionCommandHandler CreateHandler()
    {
        ZsvService.Setup(x
            => x.CreateClient(It.IsAny<Guid>()));
        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.UtcNow)
            .Returns(Constants.Time.Now);
        return new DeleteConnectionCommandHandler(
            ServerRepository.Object,
            ConnectionRepository.Object,
            ZsvServiceFactory.Object,
            mockDateTimeProvider.Object
        );
    }
}