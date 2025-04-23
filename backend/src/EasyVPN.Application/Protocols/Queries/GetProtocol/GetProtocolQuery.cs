using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Protocols.Queries.GetProtocol;

public record GetProtocolQuery(Guid ProtocolId) : IRequest<ErrorOr<Protocol>>;