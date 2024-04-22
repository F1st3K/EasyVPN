using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Protocols.Queries.GetProtocols;

public record GetProtocolsQuery() : IRequest<ErrorOr<List<Protocol>>>;