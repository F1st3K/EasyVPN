using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Queries.GetConnections;
using EasyVPN.Domain.Entities;
using Moq;

namespace EasyVPN.Application.UnitTests.Vpn.Queries.GetConnections;

public class GetConnectionsMocks
{
    public readonly Mock<IConnectionRepository> ConnectionRepository = new();
    
    public GetConnectionsQueryHandler CreateHandler()
    {
        return new GetConnectionsQueryHandler(
            ConnectionRepository.Object);
    }
}