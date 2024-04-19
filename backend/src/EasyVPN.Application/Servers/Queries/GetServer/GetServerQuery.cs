using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Servers.Queries.GetServer;

public record GetServerQuery(
    Guid ServerId
    ) : IRequest<ErrorOr<Server>>;