using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Commands.ResetLifetimeConnection;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.ResetLifetimeConnection;

public static class ResetLifetimeConnectionUtils
{
    public static ResetLifetimeConnectionCommand CreateCommand()
        => new(Constants.Connection.Id);

    public static bool IsValid(this Connection connection)
        => connection.Id == Constants.Connection.Id
           && connection.ServerId == Constants.Server.Id
           && connection.ExpirationTime == Constants.Time.Now;
}