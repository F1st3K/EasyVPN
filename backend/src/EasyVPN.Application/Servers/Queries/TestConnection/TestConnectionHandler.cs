using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Servers.Queries.TestConnection;

public class TestConnectionHandler : IRequestHandler<TestConnectionQuery, ErrorOr<Success>>
{
    private readonly IVpnServiceFactory _vpnServiceFactory;

    public TestConnectionHandler(IVpnServiceFactory vpnServiceFactory)
    {
        _vpnServiceFactory = vpnServiceFactory;
    }

    public async Task<ErrorOr<Success>> Handle(TestConnectionQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var testServer = new Server
        {
            Version = query.Version,
            ConnectionString = new ConnectionString
            {
                Auth = query.Auth,
                Endpoint = query.Endpoint
            },
        };

        if (_vpnServiceFactory.GetVpnService(testServer) is not { } vpnService)
            return Errors.Server.FailedGetService;

        return vpnService.TestConnect();
    }
}
