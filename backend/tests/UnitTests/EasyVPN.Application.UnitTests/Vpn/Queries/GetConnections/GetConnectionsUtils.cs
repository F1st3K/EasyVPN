using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Queries.GetConnections;
using EasyVPN.Domain.Entities;
using FluentAssertions;

namespace EasyVPN.Application.UnitTests.Vpn.Queries.GetConnections;

public static class GetConnectionsUtils
{
    public static void Validate(this List<Connection> result, List<Connection> valid)
        => result.Zip(valid).ToList()
            .ForEach(pair => Validate(pair.First, pair.Second));

    public static void Validate(Connection first, Connection second)
    {
        first.Id.Should().Be(second.Id);
    }
}