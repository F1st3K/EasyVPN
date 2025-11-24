using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Commands.DisableConnection;

public class DisableConnectionCommandHandler : IRequestHandler<DisableConnectionCommand, ErrorOr<Updated>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IZsvServiceFactory _zsvServiceFactory;

    public DisableConnectionCommandHandler(IConnectionRepository connectionRepository, IZsvServiceFactory zsvServiceFactory)
    {
        _connectionRepository = connectionRepository;
        _zsvServiceFactory = zsvServiceFactory;
    }

    public async Task<ErrorOr<Updated>> Handle(DisableConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (_zsvServiceFactory.GetZsvService(connection.Server) is not { } zsvService)
            return Errors.Server.FailedGetService;

        var disableResult = zsvService.DisableClient(command.ConnectionId);
        if (disableResult.IsError)
            return disableResult.ErrorsOrEmptyList;

        return Result.Updated;
    }
}
