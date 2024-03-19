using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Queries.GetConnection;

public record GetConnectionQuery(
    Guid ConnectionTicketId
    ) : IRequest<ErrorOr<Connection>>;