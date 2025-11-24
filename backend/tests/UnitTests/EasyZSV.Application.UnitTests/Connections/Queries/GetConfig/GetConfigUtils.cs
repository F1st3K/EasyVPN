using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Application.Connections.Queries.GetConfig;

namespace EasyZsV.Application.UnitTests.Connections.Queries.GetConfig;

public static class GetConfigUtils
{
    public static GetConfigQuery CreateQuery()
        => new(Constants.Connection.Id);

    public static bool IsValid(this GetConfigResult result)
        => result.ClientId == Constants.User.Id
           && result.Config == Constants.Connection.Config;
}