using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.Expire;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using ErrorOr;
using Moq;

namespace EasyVPN.Application.UnitTests.Common.Services;

public class ConnectionExpireServiceMocks
{
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IServerRepository> ServerRepository = new();
    public readonly Mock<IVpnServiceFactory> VpnServiceFactory = new();
    public readonly Mock<IVpnService> VpnService = new();
    public readonly Mock<IExpirationChecker> ExpirationChecker = new();
    
    public ConnectionExpireService Create()
    {
        ExpirationChecker.Setup(x
                => x.NewExpire(Constants.Connection.ExpirationTime, 
                    It.IsAny<Func<ErrorOr<Success>>>()))
            .Callback(new Action<DateTime, Func<ErrorOr<Success>>>(
                (expireTime, onExpire) => onExpire()));
        return new ConnectionExpireService(
            ExpirationChecker.Object,
            ConnectionRepository.Object,
            ServerRepository.Object,
            VpnServiceFactory.Object
            );
    }
}