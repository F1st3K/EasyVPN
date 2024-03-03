using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Commands.CreateConnection;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.CreateConnection;

public static class CreateConnectionUtils
{
    public static CreateConnectionCommand CreateCommand()
        => new (Constants.User.Id,
            Constants.Server.Id);

    public static bool IsValid(this Connection connection)
        => connection.ClientId == Constants.User.Id
           && connection.ServerId == Constants.Server.Id
           && connection.ExpirationTime == Constants.Connection.ExpirationTime;
}
