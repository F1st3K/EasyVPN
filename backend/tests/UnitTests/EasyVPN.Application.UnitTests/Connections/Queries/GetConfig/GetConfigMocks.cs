using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Application.Connections.Queries.GetConfig;
using Moq;

namespace EasyZsV.Application.UnitTests.Connections.Queries.GetConfig;

public class GetConfigMocks
{

    public readonly Mock<IServerRepository> ServerRepository = new();
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IZsvServiceFactory> ZsvServiceFactory = new();
    public readonly Mock<IZsvService> ZsvService = new();

    public GetConfigQueryHandler CreateHandler()
    {
        return new GetConfigQueryHandler(
            ConnectionRepository.Object,
            ZsvServiceFactory.Object);
    }
}
