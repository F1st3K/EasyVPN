using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Application.Connections.Queries.GetConnections;
using EasyZsV.Domain.Entities;
using Moq;

namespace EasyZsV.Application.UnitTests.Connections.Queries.GetConnections;

public class GetConnectionsMocks
{
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();

    public GetConnectionsQueryHandler CreateHandler()
    {
        return new GetConnectionsQueryHandler(
            ConnectionRepository.Object);
    }
}