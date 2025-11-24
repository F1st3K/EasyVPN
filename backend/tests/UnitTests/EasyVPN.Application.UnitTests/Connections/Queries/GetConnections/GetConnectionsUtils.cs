using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Application.Connections.Queries.GetConnections;
using EasyZsV.Domain.Entities;
using FluentAssertions;

namespace EasyZsV.Application.UnitTests.Connections.Queries.GetConnections;

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