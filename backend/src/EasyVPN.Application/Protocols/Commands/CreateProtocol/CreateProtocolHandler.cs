using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Protocols.Commands.CreateProtocol;

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