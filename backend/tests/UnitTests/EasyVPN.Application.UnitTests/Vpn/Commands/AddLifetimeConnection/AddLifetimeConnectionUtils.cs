using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Commands.AddLifetimeConnection;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.AddLifetimeConnection;

public static class AddLifetimeConnectionUtils
{
    public static AddLifetimeConnectionCommand CreateCommand()
        => new (Constants.Connection.Id, Constants.Connection.Days);

    public static bool ExtendIsValid(this Connection connection)
        => connection.ServerId == Constants.Server.Id
           && connection.ExpirationTime == Constants.Connection.ExpirationTime.AddDays(Constants.Connection.Days);
    
    public static bool ActivateIsValid(this Connection connection)
        => connection.ServerId == Constants.Server.Id
           && connection.ExpirationTime == Constants.Time.Now.AddDays(Constants.Connection.Days);
}
