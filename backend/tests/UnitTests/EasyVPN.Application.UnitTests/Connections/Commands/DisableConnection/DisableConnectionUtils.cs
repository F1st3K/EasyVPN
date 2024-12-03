using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Connections.Commands.DisableConnection;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.Connections.Commands.DisableConnection;

public static class DisableConnectionUtils
{
    public static DisableConnectionCommand CreateCommand()
        => new(Constants.Connection.Id);

    public static bool IsValid(this Connection connection)
        => connection.Id == Constants.Connection.Id
           && connection.Server.Id == Constants.Server.Id
           && connection.ExpirationTime == Constants.Time.Now;
}