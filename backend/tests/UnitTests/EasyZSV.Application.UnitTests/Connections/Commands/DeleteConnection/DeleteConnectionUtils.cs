using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Application.Connections.Commands.CreateConnection;
using EasyZsV.Application.Connections.Commands.DeleteConnection;
using EasyZsV.Domain.Entities;

namespace EasyZsV.Application.UnitTests.Connections.Commands.DeleteConnection;

public static class DeleteConnectionUtils
{
    public static DeleteConnectionCommand CreateCommand()
        => new(Constants.Connection.Id);
}
