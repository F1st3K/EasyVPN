using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Application.Connections.Commands.AddLifetimeConnection;
using EasyZsV.Domain.Entities;

namespace EasyZsV.Application.UnitTests.Connections.Commands.AddLifetimeConnection;

public static class AddLifetimeConnectionUtils
{
    public static AddLifetimeConnectionCommand CreateCommand()
        => new(Constants.Connection.Id, Constants.Connection.Days);

    public static bool ExtendIsValid(this Connection connection)
        => connection.Server.Id == Constants.Server.Id
           && connection.ExpirationTime == Constants.Connection.ExpirationTime.AddDays(Constants.Connection.Days);

    public static bool ActivateIsValid(this Connection connection)
        => connection.Server.Id == Constants.Server.Id
           && connection.ExpirationTime == Constants.Time.Now.AddDays(Constants.Connection.Days);
}
