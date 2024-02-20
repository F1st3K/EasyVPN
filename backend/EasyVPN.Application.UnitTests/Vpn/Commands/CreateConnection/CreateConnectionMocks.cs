using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.Vpn.Commands.CreateConnection;
using EasyVPN.Domain.Entities;
using Moq;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.CreateConnection;

public class CreateConnectionMocks
{
    public readonly Mock<IUserRepository> MockUserRepository = new();
    public readonly Mock<IUserRoleRepository> MockUserRoleRepository = new();
    public readonly Mock<IServerRepository> MockServerRepository = new();
    public readonly Mock<IConnectionRepository> MockConnectionRepository = new();
    public readonly Mock<IVpnService> MockVpnService = new();

    public CreateConnectionCommandHandler CreateHandler()
    {
        var mockVpnServiceFactory = new Mock<IVpnServiceFactory>();
        mockVpnServiceFactory.Setup(x
                => x.GetVpnService(It.IsAny<Server>()))
            .Returns(MockVpnService.Object);
        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.UtcNow)
            .Returns(DateTime.MinValue);
        return new CreateConnectionCommandHandler(
            MockUserRepository.Object,
            MockUserRoleRepository.Object,
            MockServerRepository.Object,
            MockConnectionRepository.Object,
            mockVpnServiceFactory.Object,
            mockDateTimeProvider.Object
        );
    }
}