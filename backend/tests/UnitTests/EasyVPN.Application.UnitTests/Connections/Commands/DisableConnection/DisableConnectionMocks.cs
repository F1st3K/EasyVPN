using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.Connections.Commands.DisableConnection;
using EasyVPN.Domain.Entities;
using Moq;

namespace EasyVPN.Application.UnitTests.Connections.Commands.DisableConnection;

public class DisableConnectionMocks
{
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IVpnServiceFactory> VpnServiceFactory = new();
    public readonly Mock<IVpnService> VpnService = new();

    public DisableConnectionCommandHandler CreateHandler()
    {
        return new DisableConnectionCommandHandler(
            ConnectionRepository.Object,
            VpnServiceFactory.Object
        );
    }
}