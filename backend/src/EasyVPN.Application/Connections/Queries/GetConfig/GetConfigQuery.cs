using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Connections.Queries.GetConfig;

public record GetConfigQuery(
    Guid ConnectionId
    ) : IRequest<ErrorOr<GetConfigResult>>;