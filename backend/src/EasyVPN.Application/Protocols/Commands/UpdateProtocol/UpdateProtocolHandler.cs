using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Protocols.Commands.UpdateProtocol;

public class UpdateProtocolsHandler : IRequestHandler<UpdateProtocolCommand, ErrorOr<Updated>>
{
    private readonly IProtocolRepository _protocolRepository;

    public UpdateProtocolsHandler(IProtocolRepository protocolRepository)
    {
        _protocolRepository = protocolRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateProtocolCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_protocolRepository.Get(command.ProtocolId) is not { } protocol)
            return Errors.Protocol.NotFound;
        
        protocol.Name = command.Name;
        protocol.Icon = command.Icon;
            
        _protocolRepository.Update(protocol);

        return Result.Updated;
    }
}