using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Commands.DeleteConnection;

public class DeleteConnectionCommandHandler : IRequestHandler<DeleteConnectionCommand, ErrorOr<Success>>
{
    private readonly IServerRepository _serverRepository;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public DeleteConnectionCommandHandler(
        IServerRepository serverRepository,
        IConnectionRepository connectionRepository,
        IVpnServiceFactory vpnServiceFactory, 
        IDateTimeProvider dateTimeProvider)
    {
        _serverRepository = serverRepository;
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<ErrorOr<Success>> Handle(DeleteConnectionCommand command, CancellationToken cancellationToken)
    {   
        await Task.CompletedTask;
        
        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        if (connection.ExpirationTime > _dateTimeProvider.UtcNow)
            return Errors.Connection.NotExpired;
        
        if (_serverRepository.Get(connection.ServerId) is not { } server)
            return Errors.Server.NotFound;

        if (_vpnServiceFactory.GetVpnService(server) is not { } vpnService)
            return Errors.Server.FailedGetService;
        
        vpnService.DeleteClient(connection.Id);
        _connectionRepository.Remove(connection.Id);
        
        return new Success();
    }
}