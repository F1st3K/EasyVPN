using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Connections.Commands.CreateConnection;
using EasyVPN.Application.Connections.Commands.DeleteConnection;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.Connections.Commands.DeleteConnection;

public static class DeleteConnectionUtils
{
    public static DeleteConnectionCommand CreateCommand()
        => new (Constants.Connection.Id);
}
