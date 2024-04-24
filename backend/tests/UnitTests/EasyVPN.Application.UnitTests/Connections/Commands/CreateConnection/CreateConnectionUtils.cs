using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Connections.Commands.CreateConnection;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.Connections.Commands.CreateConnection;

public static class CreateConnectionUtils
{
    public static CreateConnectionCommand CreateCommand()
        => new (Constants.User.Id,
            Constants.Server.Id);

    public static bool IsValid(this Connection connection)
        => connection.Client.Id == Constants.User.Id
           && connection.Server.Id == Constants.Server.Id
           && connection.ExpirationTime == Constants.Time.Now;
}
