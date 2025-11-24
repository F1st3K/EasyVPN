using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Application.Connections.Commands.CreateConnection;
using EasyZsV.Domain.Entities;

namespace EasyZsV.Application.UnitTests.Connections.Commands.CreateConnection;

public static class CreateConnectionUtils
{
    public static CreateConnectionCommand CreateCommand()
        => new(Constants.User.Id,
            Constants.Server.Id);

    public static bool IsValid(this Connection connection)
        => connection.Client.Id == Constants.User.Id
           && connection.Server.Id == Constants.Server.Id
           && connection.ExpirationTime == Constants.Time.Now;
}
