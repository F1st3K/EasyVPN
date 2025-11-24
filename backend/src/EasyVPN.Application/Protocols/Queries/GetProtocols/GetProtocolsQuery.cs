using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Protocols.Queries.GetProtocols;

public record GetProtocolsQuery() : IRequest<ErrorOr<List<Protocol>>>;