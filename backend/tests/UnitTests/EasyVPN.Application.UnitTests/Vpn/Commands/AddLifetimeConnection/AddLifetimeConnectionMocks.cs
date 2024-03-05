using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.Common.Service;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Commands.AddLifetimeConnection;
using Moq;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.AddLifetimeConnection;

public class AddLifetimeConnectionMocks
{
    public readonly Mock<IServerRepository> ServerRepository = new();
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IVpnServiceFactory> VpnServiceFactory = new();
    public readonly Mock<IVpnService> VpnService = new();
    public readonly Mock<IConnectionExpireService> ExpireService = new();

    public AddLifetimeConnectionCommandHandler CreateHandler()
    {
        
        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.UtcNow)
            .Returns(Constants.Time.Now);
        return new AddLifetimeConnectionCommandHandler(
            ConnectionRepository.Object,
            ServerRepository.Object,
            VpnServiceFactory.Object,
            mockDateTimeProvider.Object,
            ExpireService.Object
        );
    }
}