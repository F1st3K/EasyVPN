using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Commands.CreateConnection;

namespace EasyVPN.Application.UnitTests.Vpn.TestUtils;

public static class CreateConnectionUtils
{
    public static CreateConnectionCommand CreateCommand()
        => new CreateConnectionCommand(
            Constants.User.ClientId,
            Constants.Server.Id,
            Constants.Connection.Days);
}