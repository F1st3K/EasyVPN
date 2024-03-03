using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.Common.Services;

public static class ConnectionExpireServiceUtils
{
    public static Connection GetConnection()
        => new()
        {
            Id = Constants.Connection.Id,
            ServerId = Constants.Server.Id,
            ExpirationTime = Constants.Connection.ExpirationTime,
            Status = ConnectionStatus.Pending
        };
    
    public static bool IsValid(this Connection connection)
        => connection.Id == Constants.Connection.Id
           && connection.ServerId == Constants.Server.Id
           && connection.ExpirationTime == Constants.Connection.ExpirationTime
           && connection.Status == ConnectionStatus.Expired;
}