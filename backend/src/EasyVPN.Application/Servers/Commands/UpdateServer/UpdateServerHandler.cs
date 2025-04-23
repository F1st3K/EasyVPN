using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Servers.Commands.UpdateServer;

public class UpdateServerHandler : IRequestHandler<UpdateServerCommand, ErrorOr<Created>>
{
    private readonly IServerRepository _serverRepository;
    private readonly IProtocolRepository _protocolRepository;

    public UpdateServerHandler(IServerRepository serverRepository, IProtocolRepository protocolRepository)
    {
        _serverRepository = serverRepository;
        _protocolRepository = protocolRepository;
    }

    public async Task<ErrorOr<Created>> Handle(UpdateServerCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (_serverRepository.Get(command.ServerId) is not { } server)
            return Errors.Server.NotFound;

        if (_protocolRepository.Get(command.ProtocolId) is not { } protocol)
            return Errors.Protocol.NotFound;

        server.Id = command.ServerId;
        server.ConnectionString = command.ConnectionString;
        server.Protocol = protocol;
        server.Version = command.Version;
                
        _serverRepository.Update(server);
        
        return Result.Created;
    }
}
