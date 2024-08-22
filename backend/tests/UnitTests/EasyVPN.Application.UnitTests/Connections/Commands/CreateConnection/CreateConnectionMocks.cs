using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Connections.Commands.CreateConnection;
using Moq;

namespace EasyVPN.Application.UnitTests.Connections.Commands.CreateConnection;

public class CreateConnectionMocks
{
    public readonly Mock<IUserRepository> UserRepository = new();
    public readonly Mock<IServerRepository> ServerRepository = new();
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IVpnServiceFactory> VpnServiceFactory = new();
    public readonly Mock<IVpnService> VpnService = new();

    public CreateConnectionCommandHandler CreateHandler()
    {
        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.UtcNow)
            .Returns(Constants.Time.Now);
        return new CreateConnectionCommandHandler(
            UserRepository.Object,
            ServerRepository.Object,
            ConnectionRepository.Object,
            VpnServiceFactory.Object,
            mockDateTimeProvider.Object
        );
    }
}
