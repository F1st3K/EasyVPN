using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Commands.CreateConnection;
using EasyVPN.Application.Vpn.Commands.DeleteConnection;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.DeleteConnection;

public static class DeleteConnectionUtils
{
    public static DeleteConnectionCommand CreateCommand()
        => new (Constants.Connection.Id);
}
