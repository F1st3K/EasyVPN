using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Application.Connections.Commands.DisableConnection;
using EasyZsV.Domain.Entities;
using Moq;

namespace EasyZsV.Application.UnitTests.Connections.Commands.DisableConnection;

public class DisableConnectionMocks
{
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    public readonly Mock<IZsvServiceFactory> ZsvServiceFactory = new();
    public readonly Mock<IZsvService> ZsvService = new();

    public DisableConnectionCommandHandler CreateHandler()
    {
        return new DisableConnectionCommandHandler(
            ConnectionRepository.Object,
            ZsvServiceFactory.Object
        );
    }
}