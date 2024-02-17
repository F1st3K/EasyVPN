using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.UnitTests.Vpn.TestUtils;
using EasyVPN.Application.Vpn.Commands.CreateConnection;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.CreateConnection;

public class CreateConnectionTests
{
    public async Task HandleCreateConnectionCommand()
    {
        var command = new CreateConnectionCommand(
            Constants.User.ClientId,
            Constants.Server.Id,
            Constants.Connection.Days);
        var hendler = new CreateConnectionCommandHandler();
    }
}