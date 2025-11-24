using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Queries.GetConfig;

public class GetConfigQueryHandler : IRequestHandler<GetConfigQuery, ErrorOr<GetConfigResult>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IZsvServiceFactory _zsvServiceFactory;

    public GetConfigQueryHandler(
        IConnectionRepository connectionRepository,
        IZsvServiceFactory zsvServiceFactory)
    {
        _connectionRepository = connectionRepository;
        _zsvServiceFactory = zsvServiceFactory;
    }

    public async Task<ErrorOr<GetConfigResult>> Handle(GetConfigQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(query.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (_zsvServiceFactory.GetZsvService(connection.Server) is not { } zsvService)
            return Errors.Server.FailedGetService;

        var configResult = zsvService.GetConfig(query.ConnectionId);
        if (configResult.IsError)
            return configResult.ErrorsOrEmptyList;

        return new GetConfigResult(connection.Client.Id, configResult.Value);
    }
}
