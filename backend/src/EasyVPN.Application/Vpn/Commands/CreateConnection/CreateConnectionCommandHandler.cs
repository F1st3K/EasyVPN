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
    
    public async Task<ErrorOr<Success>> Handle(CreateConnectionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (_userRepository.GetUserById(command.UserId) is not { } user)
            return Errors.User.NotFound;

        if (_serverRepository.Get(command.ServerId) is not { } server)
            return Errors.Server.NotFound;

        var connection = new Connection()
        {
            Id = Guid.NewGuid(),
            ClientId = user.Id,
            ExpirationTime = _dateTimeProvider.UtcNow.AddDays(command.CountDays),
            ServerId = server.Id,
            Status = ConnectionStatus.Pending
        };
        _connectionRepository.Add(connection);
        _vpnServiceFactory.GetVpnService(server)
            .CreateClient(connection);
        
        return new Success();
    }
}