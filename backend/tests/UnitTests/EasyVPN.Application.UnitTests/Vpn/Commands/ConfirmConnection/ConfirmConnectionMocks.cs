using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.Common.Service;
using EasyVPN.Application.Vpn.Commands.ConfirmConnection;
using Moq;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.ConfirmConnection;

public class ConfirmConnectionMocks
{
    public readonly Mock<IServerRepository> ServerRepository = new();
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IVpnServiceFactory> VpnServiceFactory = new();
    public readonly Mock<IVpnService> VpnService = new();
    public readonly Mock<IConnectionExpireService> ExpireService = new();

    public ConfirmConnectionCommandHandler CreateHandler()
    {
        
        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.UtcNow)
            .Returns(DateTime.MinValue);
        return new ConfirmConnectionCommandHandler(
            ConnectionRepository.Object,
            ServerRepository.Object,
            VpnServiceFactory.Object,
            mockDateTimeProvider.Object,
            ExpireService.Object
        );
    }
}