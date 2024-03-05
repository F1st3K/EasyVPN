using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Commands.CreateConnection;
using EasyVPN.Application.Vpn.Commands.DeleteConnection;
using EasyVPN.Domain.Entities;
using Moq;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.DeleteConnection;

public class DeleteConnectionMocks
{
    public readonly Mock<IServerRepository> ServerRepository = new();
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IVpnServiceFactory> VpnServiceFactory = new();
    public readonly Mock<IVpnService> VpnService = new();

    public DeleteConnectionCommandHandler CreateHandler()
    {
        VpnService.Setup(x
            => x.CreateClient(It.IsAny<Connection>()));
        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.UtcNow)
            .Returns(Constants.Time.Now);
        return new DeleteConnectionCommandHandler(
            ServerRepository.Object,
            ConnectionRepository.Object,
            VpnServiceFactory.Object,
            mockDateTimeProvider.Object
        );
    }
}