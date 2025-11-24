using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Services;
using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Commands.DeleteConnection;

public class DeleteConnectionCommandHandler : IRequestHandler<DeleteConnectionCommand, ErrorOr<Deleted>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IZsvServiceFactory _zsvServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public DeleteConnectionCommandHandler(
        IServerRepository serverRepository,
        IConnectionRepository connectionRepository,
        IZsvServiceFactory zsvServiceFactory,
        IDateTimeProvider dateTimeProvider)
    {
        _connectionRepository = connectionRepository;
        _zsvServiceFactory = zsvServiceFactory;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (connection.ExpirationTime > _dateTimeProvider.UtcNow)
            return Errors.Connection.NotExpired;

        if (_zsvServiceFactory.GetZsvService(connection.Server) is not { } zsvService)
            return Errors.Server.FailedGetService;

        var deleteResult = zsvService.DeleteClient(connection.Id);
        if (deleteResult.IsError)
            return deleteResult.ErrorsOrEmptyList;

        _connectionRepository.Remove(connection.Id);

        return Result.Deleted;
    }
}
