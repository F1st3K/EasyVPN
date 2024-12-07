using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Connections.Commands.DisableConnection;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Commands.ResetLifetimeConnection;

public class ResetLifetimeConnectionCommandHandler : IRequestHandler<ResetLifetimeConnectionCommand, ErrorOr<Updated>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITaskRepository _taskRepository;
    private readonly ISender _sender;

    public ResetLifetimeConnectionCommandHandler(
        IConnectionRepository connectionRepository,
        IDateTimeProvider dateTimeProvider,
        ITaskRepository taskRepository,
        ISender sender)
    {
        _connectionRepository = connectionRepository;
        _dateTimeProvider = dateTimeProvider;
        _taskRepository = taskRepository;
        _sender = sender;
    }

    public async Task<ErrorOr<Updated>> Handle(ResetLifetimeConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        connection.ExpirationTime = _dateTimeProvider.UtcNow;
        _connectionRepository.Update(connection);

        if (_taskRepository.PopTask<DisableConnectionCommand>(connection.Id) is not {} disableCommand)
            disableCommand = new DisableConnectionCommand(connection.Id);
                
        var disableResult = await _sender.Send(disableCommand, cancellationToken);
        
        if (disableResult.IsError)
            return disableResult.ErrorsOrEmptyList;
        return Result.Updated;
    }
}
