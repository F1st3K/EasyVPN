using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Commands.CreateConnection;

public class CreateConnectionCommandHandler : IRequestHandler<CreateConnectionCommand, ErrorOr<Connection>>
{
    private readonly IUserRepository _userRepository;
    private readonly IServerRepository _serverRepository;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateConnectionCommandHandler(
        IUserRepository userRepository,
        IServerRepository serverRepository,
        IConnectionRepository connectionRepository,
        IVpnServiceFactory vpnServiceFactory, 
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _serverRepository = serverRepository;
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
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

        if (_vpnServiceFactory.GetVpnService(server) is not { } vpnService)
            return Errors.Server.FailedGetService;
        
        var connection = new Connection()
        {
            Id = Guid.NewGuid(),
            Client = user,
            ExpirationTime = _dateTimeProvider.UtcNow,
            ServerId = server.Id
        };
        _connectionRepository.Add(connection);
        vpnService.CreateClient(connection.Id);
        
        return connection;
    }
}