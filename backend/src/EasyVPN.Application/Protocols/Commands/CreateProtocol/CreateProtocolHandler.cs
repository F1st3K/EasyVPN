using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Protocols.Commands.CreateProtocol;

public class CreateProtocolsHandler : IRequestHandler<CreateProtocolCommand, ErrorOr<Created>>
{
    private readonly IProtocolRepository _protocolRepository;

    public CreateProtocolsHandler(IProtocolRepository protocolRepository)
    {
        _protocolRepository = protocolRepository;
    }

    public async Task<ErrorOr<Created>> Handle(CreateProtocolCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var protocol = new Protocol
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Icon = command.Icon,
        };
        _protocolRepository.Create(protocol);

        return Result.Created;
    }
}