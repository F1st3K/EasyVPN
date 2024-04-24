using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Connections.Queries.GetConfig;
using Moq;

namespace EasyVPN.Application.UnitTests.Connections.Queries.GetConfig;

public class GetConfigMocks
{
    
    public readonly Mock<IServerRepository> ServerRepository = new();
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IVpnServiceFactory> VpnServiceFactory = new();
    public readonly Mock<IVpnService> VpnService = new();
    
    public GetConfigQueryHandler CreateHandler()
    {
        VpnService.Setup(x =>
                x.GetConfig(It.IsAny<Guid>()))
            .Returns(Constants.Connection.Config);
        return new GetConfigQueryHandler(
            ConnectionRepository.Object,
            VpnServiceFactory.Object);
    }
}