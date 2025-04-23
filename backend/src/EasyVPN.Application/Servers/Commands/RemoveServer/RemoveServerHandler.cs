using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Connections.Commands.DisableConnection;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Servers.Commands.RemoveServer;

public class RemoveServerHandler : IRequestHandler<RemoveServerCommand, ErrorOr<Deleted>>
{
    private readonly IServerRepository _serverRepository;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITaskRepository _taskRepository;
    private readonly ISender _sender;

    public RemoveServerHandler(
        IServerRepository serverRepository,
        IConnectionRepository connectionRepository,
        IDateTimeProvider dateTimeProvider,
        ITaskRepository taskRepository,
        ISender sender)
    {
        _serverRepository = serverRepository;
        _connectionRepository = connectionRepository;
        _dateTimeProvider = dateTimeProvider;
        _taskRepository = taskRepository;
        _sender = sender;
    }

    public async Task<ErrorOr<Deleted>> Handle(RemoveServerCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_serverRepository.Get(command.ServerId) is not { } server)
            return Errors.Server.NotFound;

        var serverConnections = _connectionRepository.GetAll()
            .Where(c => c.Server.Id == command.ServerId).ToList();

        if (serverConnections.Any(c => c.ExpirationTime > _dateTimeProvider.UtcNow))
            return Errors.Server.StillInUseActive;

        foreach (var c in serverConnections)
        {
            if (_taskRepository.TryPopTask<DisableConnectionCommand>(c.Id) is not { } disableCommand)
                disableCommand = new DisableConnectionCommand(c.Id);
            await _sender.Send(disableCommand, cancellationToken);
        }
        _serverRepository.Remove(server.Id);

        return Result.Deleted;
    }
}
