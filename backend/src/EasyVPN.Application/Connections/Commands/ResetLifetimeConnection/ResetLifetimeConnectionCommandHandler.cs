using EasyVPN.Application.Common.Interfaces.Expire;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.Connections.Commands.DisableConnection;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Commands.ResetLifetimeConnection;

public class ResetLifetimeConnectionCommandHandler : IRequestHandler<ResetLifetimeConnectionCommand, ErrorOr<Updated>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITaskRepository _taskRepository;
    private readonly ISender _sender;

    public ResetLifetimeConnectionCommandHandler(
        IConnectionRepository connectionRepository,
        IServerRepository serverRepository,
        IVpnServiceFactory vpnServiceFactory,
        IDateTimeProvider dateTimeProvider,
        ITaskRepository taskRepository,
        ISender sender)
    {
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
        _dateTimeProvider = dateTimeProvider;
        _taskRepository = taskRepository;
        _sender = sender;
    }

    public async Task<ErrorOr<Updated>> Handle(ResetLifetimeConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (_vpnServiceFactory.GetVpnService(connection.Server) is not { } vpnService)
            return Errors.Server.FailedGetService;

        connection.ExpirationTime = _dateTimeProvider.UtcNow;
        _connectionRepository.Update(connection);

        if (_taskRepository.PopTask<DisableConnectionCommand>(connection.Id) is not {} disableCommand)
            disableCommand = new DisableConnectionCommand(connection.Id, connection.Server);
                
        var disableResult = await _sender.Send(disableCommand, cancellationToken);
        
        if (disableResult.IsError)
            return disableResult.ToErrorOr().ErrorsOrEmptyList;
        return Result.Updated;
    }
}
