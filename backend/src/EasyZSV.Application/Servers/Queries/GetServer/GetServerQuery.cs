using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Servers.Queries.GetServer;

public record GetServerQuery(
    Guid ServerId
    ) : IRequest<ErrorOr<Server>>;