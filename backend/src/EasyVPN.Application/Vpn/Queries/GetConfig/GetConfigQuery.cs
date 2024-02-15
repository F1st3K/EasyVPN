using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Queries.GetConfig;

public record GetConfigQuery(
    Guid ConnectionId
    ) : IRequest<ErrorOr<GetConfigResult>>;