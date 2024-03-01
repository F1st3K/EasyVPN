using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Commands.ActivateConnection;

public class ActivateConnectionCommandHandler : IRequestHandler<ActivateConnectionCommand, ErrorOr<Success>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IVpnServiceFactory _vpnServiceFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ActivateConnectionCommandHandler(
        IConnectionRepository connectionRepository,
        IVpnServiceFactory vpnServiceFactory, 
        IDateTimeProvider dateTimeProvider)
    {
        _connectionRepository = connectionRepository;
        _vpnServiceFactory = vpnServiceFactory;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<ErrorOr<Success>> Handle(ActivateConnectionCommand command, CancellationToken cancellationToken)
    {   
        await Task.CompletedTask;
        
        // TODO: Create him
        
        return new Success();
    }
}