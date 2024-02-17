using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.CommonTestUtils.Dependencies.Vpn;

public class StubVpnServiceFactory : IVpnServiceFactory
{
    public IVpnService GetVpnService(Server server)
    {
        return new StubVpnService(server);
    }

    public class StubVpnService : IVpnService
    {
        private readonly Server _server;
        
        public StubVpnService(Server server)
        {
            _server = server;
        }
        
        public string GetConfig(Guid connectionId)
        {
            return $"{_server.Host}/?password=1234";
        }

        public void CreateClient(Connection connection)
        {
            
        }

        public void EnableClient(Guid connectionId)
        {
            
        }

        public void DisableClient(Guid connectionId)
        {
            
        }

        public void DeleteClient(Guid connectionId)
        {
            
        }
    }
}