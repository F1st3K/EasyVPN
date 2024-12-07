using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.Connections.Commands.DisableConnection;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Commands.AddLifetimeConnection;

public class AddLifetimeConnectionCommandHandler : IRequestHandler<AddLifetimeConnectionCommand, ErrorOr<Updated>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITaskRepository _taskRepository;

    public AddLifetimeConnectionCommandHandler(
        IConnectionRepository connectionRepository,
        IVpnServiceFactory vpnServiceFactory,
        IDateTimeProvider dateTimeProvider,
        ITaskRepository taskRepository)
    {
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
        _dateTimeProvider = dateTimeProvider;
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(AddLifetimeConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (_vpnServiceFactory.GetVpnService(connection.Server) is not { } vpnService)
            return Errors.Server.FailedGetService;

        if (connection.ExpirationTime < _dateTimeProvider.UtcNow)
            connection.ExpirationTime = _dateTimeProvider.UtcNow;

        connection.ExpirationTime =
            connection.ExpirationTime.AddDays(command.CountDays);
        _connectionRepository.Update(connection);

        var enableResult = vpnService.EnableClient(connection.Id);
        if (enableResult.IsError)
            return enableResult.ErrorsOrEmptyList;

        if (_taskRepository.PopTask<DisableConnectionCommand>(connection.Id) is not {} disableCommand)
            disableCommand = new DisableConnectionCommand(connection.Id);
                
        _taskRepository.PushTask(connection.Id, connection.ExpirationTime, disableCommand);
        
        return Result.Updated;
    }
}
