using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Protocols.Commands.RemoveProtocol;

public class RemoveProtocolsHandler : IRequestHandler<RemoveProtocolCommand, ErrorOr<Deleted>>
{
    private readonly IProtocolRepository _protocolRepository;
    private readonly IServerRepository _serverRepository;

    public RemoveProtocolsHandler(IProtocolRepository protocolRepository, IServerRepository serverRepository)
    {
        _protocolRepository = protocolRepository;
        _serverRepository = serverRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(RemoveProtocolCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_protocolRepository.Get(command.ProtocolId) is not { } protocol)
            return Errors.Protocol.NotFound;

        if (_serverRepository.GetAll().Any(s => s.Protocol.Id == protocol.Id))
            return Errors.Protocol.StillInUse;

        _protocolRepository.Remove(protocol.Id);

        return Result.Deleted;
    }
}