using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Protocols.Queries.GetProtocol;

public record GetProtocolQuery(Guid ProtocolId) : IRequest<ErrorOr<Protocol>>;