using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Services;
using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Domain.Common.Enums;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Commands.CreateConnection;

public class CreateConnectionCommandHandler : IRequestHandler<CreateConnectionCommand, ErrorOr<Connection>>
{
    private readonly IUserRepository _userRepository;
    private readonly IServerRepository _serverRepository;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IZsvServiceFactory _zsvServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateConnectionCommandHandler(
        IUserRepository userRepository,
        IServerRepository serverRepository,
        IConnectionRepository connectionRepository,
        IZsvServiceFactory zsvServiceFactory,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _serverRepository = serverRepository;
        _connectionRepository = connectionRepository;
        _zsvServiceFactory = zsvServiceFactory;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Connection>> Handle(CreateConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetById(command.ClientId) is not { } user)
            return Errors.User.NotFound;

        if (user.Roles.Any(r => r == RoleType.Client) == false)
            return Errors.Access.ClientsOnly;

        if (_serverRepository.Get(command.ServerId) is not { } server)
            return Errors.Server.NotFound;

        if (_zsvServiceFactory.GetZsvService(server) is not { } zsvService)
            return Errors.Server.FailedGetService;

        var connection = new Connection()
        {
            Id = Guid.NewGuid(),
            Client = user,
            ExpirationTime = _dateTimeProvider.UtcNow,
            Server = server,
        };
        var createResult = zsvService.CreateClient(connection.Id);
        if (createResult.IsError)
            return createResult.ErrorsOrEmptyList;

        _connectionRepository.Add(connection);

        return connection;
    }
}
