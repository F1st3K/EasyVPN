using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Protocols.Queries.GetProtocol;

public class GetProtocolsHandler : IRequestHandler<GetProtocolQuery, ErrorOr<Protocol>>
{
    private readonly IProtocolRepository _protocolRepository;

    public GetProtocolsHandler(IProtocolRepository protocolRepository)
    {
        _protocolRepository = protocolRepository;
    }

    public async Task<ErrorOr<Protocol>> Handle(GetProtocolQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_protocolRepository.Get(query.ProtocolId) is not { } protocol)
            return Errors.Protocol.NotFound;

        return protocol;
    }
}