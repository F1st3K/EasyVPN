using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Protocols.Queries.GetProtocols;

public class GetProtocolsHandler : IRequestHandler<GetProtocolsQuery, ErrorOr<List<Protocol>>>
{
    private readonly IProtocolRepository _protocolRepository;

    public GetProtocolsHandler(IProtocolRepository protocolRepository)
    {
        _protocolRepository = protocolRepository;
    }

    public async Task<ErrorOr<List<Protocol>>> Handle(GetProtocolsQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var protocols = _protocolRepository.GetAll();

        return protocols.ToList();
    }
}