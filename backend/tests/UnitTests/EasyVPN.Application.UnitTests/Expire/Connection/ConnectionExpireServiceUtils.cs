using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;

namespace EasyVPN.Application.UnitTests.Expire.Connection;

public static class ConnectionExpireServiceUtils
{
    public static Domain.Entities.Connection GetConnection()
        => new()
        {
            Id = Constants.Connection.Id,
            ServerId = Constants.Server.Id,
            ExpirationTime = Constants.Connection.ExpirationTime
        };
}