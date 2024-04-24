using EasyVPN.Application.Common.Interfaces.Expire;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Connections.Commands.AddLifetimeConnection;
using EasyVPN.Domain.Entities;
using Moq;

namespace EasyVPN.Application.UnitTests.Connections.Commands.AddLifetimeConnection;

public class AddLifetimeConnectionMocks
{
    public readonly Mock<IServerRepository> ServerRepository = new();
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IVpnServiceFactory> VpnServiceFactory = new();
    public readonly Mock<IVpnService> VpnService = new();
    public readonly Mock<IExpireService<Connection>> ExpireService = new();

    public AddLifetimeConnectionCommandHandler CreateHandler()
    {
        
        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.UtcNow)
            .Returns(Constants.Time.Now);
        return new AddLifetimeConnectionCommandHandler(
            ConnectionRepository.Object,
            VpnServiceFactory.Object,
            mockDateTimeProvider.Object,
            ExpireService.Object
        );
    }
}