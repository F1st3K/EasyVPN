using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Services;
using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Application.Connections.Commands.DisableConnection;
using EasyZsV.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Commands.AddLifetimeConnection;

public class AddLifetimeConnectionCommandHandler : IRequestHandler<AddLifetimeConnectionCommand, ErrorOr<Updated>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IZsvServiceFactory _zsvServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITaskRepository _taskRepository;

    public AddLifetimeConnectionCommandHandler(
        IConnectionRepository connectionRepository,
        IZsvServiceFactory zsvServiceFactory,
        IDateTimeProvider dateTimeProvider,
        ITaskRepository taskRepository)
    {
        _connectionRepository = connectionRepository;
        _zsvServiceFactory = zsvServiceFactory;
        _dateTimeProvider = dateTimeProvider;
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(AddLifetimeConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (_zsvServiceFactory.GetZsvService(connection.Server) is not { } zsvService)
            return Errors.Server.FailedGetService;

        if (connection.ExpirationTime < _dateTimeProvider.UtcNow)
            connection.ExpirationTime = _dateTimeProvider.UtcNow;

        connection.ExpirationTime =
            connection.ExpirationTime.AddDays(command.CountDays);
        _connectionRepository.Update(connection);

        var enableResult = zsvService.EnableClient(connection.Id);
        if (enableResult.IsError)
            return enableResult.ErrorsOrEmptyList;

        if (_taskRepository.TryPopTask<DisableConnectionCommand>(connection.Id) is not { } disableCommand)
            disableCommand = new DisableConnectionCommand(connection.Id);

        _taskRepository.PushTask(connection.Id, connection.ExpirationTime, disableCommand);

        return Result.Updated;
    }
}
