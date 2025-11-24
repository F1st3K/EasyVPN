using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Servers.Commands.CreateServer;

public class CreateServerHandler : IRequestHandler<CreateServerCommand, ErrorOr<Created>>
{
    private readonly IServerRepository _serverRepository;
    private readonly IProtocolRepository _protocolRepository;

    public CreateServerHandler(IServerRepository serverRepository, IProtocolRepository protocolRepository)
    {
        _serverRepository = serverRepository;
        _protocolRepository = protocolRepository;
    }

    public async Task<ErrorOr<Created>> Handle(CreateServerCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_protocolRepository.Get(command.ProtocolId) is not { } protocol)
            return Errors.Protocol.NotFound;

        var server = new Server
        {
            Id = Guid.NewGuid(),
            ConnectionString = command.ConnectionString,
            Protocol = protocol,
            Version = command.Version
        };
        _serverRepository.Add(server);

        return Result.Created;
    }
}
