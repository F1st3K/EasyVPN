using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Connections.Queries.GetConfig;

public record GetConfigQuery(
    Guid ConnectionId
    ) : IRequest<ErrorOr<GetConfigResult>>;