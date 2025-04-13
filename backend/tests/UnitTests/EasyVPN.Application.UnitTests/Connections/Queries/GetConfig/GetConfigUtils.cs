using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Connections.Queries.GetConfig;

namespace EasyVPN.Application.UnitTests.Connections.Queries.GetConfig;

public static class GetConfigUtils
{
    public static GetConfigQuery CreateQuery()
        => new(Constants.Connection.Id);

    public static bool IsValid(this GetConfigResult result)
        => result.ClientId == Constants.User.Id
           && result.Config == Constants.Connection.Config;
}