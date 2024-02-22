using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Queries.GetConfig;

namespace EasyVPN.Application.UnitTests.Vpn.Queries.GetConfig;

public static class GetConfigUtils
{
    public static GetConfigQuery CreateQuery()
        => new (Constants.Connection.Id);

    public static bool IsValid(this GetConfigResult result)
        => result.ClientId == Constants.User.Id
           && result.Config == Constants.Connection.Config;
}