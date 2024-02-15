using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Commands.CreateConnection;

public class CreateConnectionCommandHandler : IRequestHandler<CreateConnectionCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IServerRepository _serverRepository;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateConnectionCommandHandler(
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IServerRepository serverRepository,
        IConnectionRepository connectionRepository,
        IVpnServiceFactory vpnServiceFactory, 
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _serverRepository = serverRepository;
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<ErrorOr<Success>> Handle(CreateConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (_userRepository.GetUserById(command.ClientId) is not { } user)
            return Errors.User.NotFound;

        if (_userRoleRepository.GetRolesByUserId(command.ClientId)
                .Any(r => r == RoleType.Client) == false)
            return Errors.Access.ClientsOnly;

        if (_serverRepository.Get(command.ServerId) is not { } server)
            return Errors.Server.NotFound;

        if (_vpnServiceFactory.GetVpnService(server) is not { } vpnService)
            return Errors.Server.FailedGetService;
        
        var connection = new Connection()
        {
            Id = Guid.NewGuid(),
            ClientId = user.Id,
            ExpirationTime = _dateTimeProvider.UtcNow.AddDays(command.CountDays),
            ServerId = server.Id,
            Status = ConnectionStatus.Pending
        };
        _connectionRepository.Add(connection);
        vpnService.CreateClient(connection);
        
        return new Success();
    }
}