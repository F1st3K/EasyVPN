using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Commands.ConfirmConnection;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.ConfirmConnection;

public static class ConfirmConnectionUtils
{
    public static ConfirmConnectionCommand CreateCommand()
        => new (Constants.Connection.Id, Constants.Connection.Days);

    public static bool IsValid(this Connection connection)
        => connection.Status == ConnectionStatus.Active
           && connection.ServerId == Constants.Server.Id
           && connection.ExpirationTime == DateTime.MinValue.AddDays(Constants.Connection.Days);
}
