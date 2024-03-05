using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;

namespace EasyVPN.Application.UnitTests.Expire.Connection;

public static class ConnectionExpireServiceUtils
{
    public static Domain.Entities.Connection GetConnection()
        => new()
        {
            Id = Constants.Connection.Id,
            ServerId = Constants.Server.Id,
            ExpirationTime = Constants.Connection.ExpirationTime,
            IsActive = false
        };
    
    public static bool IsValid(this Domain.Entities.Connection connection)
        => connection.Id == Constants.Connection.Id
           && connection.ServerId == Constants.Server.Id
           && connection.ExpirationTime == Constants.Connection.ExpirationTime
           && connection.IsActive == false;
}