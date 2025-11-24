using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Services;
using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Application.Connections.Commands.CreateConnection;
using Moq;

namespace EasyZsV.Application.UnitTests.Connections.Commands.CreateConnection;

public class CreateConnectionMocks
{
    public readonly Mock<IUserRepository> UserRepository = new();
    public readonly Mock<IServerRepository> ServerRepository = new();
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IZsvServiceFactory> ZsvServiceFactory = new();
    public readonly Mock<IZsvService> ZsvService = new();

    public CreateConnectionCommandHandler CreateHandler()
    {
        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.UtcNow)
            .Returns(Constants.Time.Now);
        return new CreateConnectionCommandHandler(
            UserRepository.Object,
            ServerRepository.Object,
            ConnectionRepository.Object,
            ZsvServiceFactory.Object,
            mockDateTimeProvider.Object
        );
    }
}
