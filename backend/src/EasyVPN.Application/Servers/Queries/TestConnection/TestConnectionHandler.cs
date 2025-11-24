using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Servers.Queries.TestConnection;

public class TestConnectionHandler : IRequestHandler<TestConnectionQuery, ErrorOr<Success>>
{
    private readonly IZsvServiceFactory _zsvServiceFactory;

    public TestConnectionHandler(IZsvServiceFactory zsvServiceFactory)
    {
        _zsvServiceFactory = zsvServiceFactory;
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

        if (_zsvServiceFactory.GetZsvService(testServer) is not { } zsvService)
            return Errors.Server.FailedGetService;

        return zsvService.TestConnect();
    }
}
