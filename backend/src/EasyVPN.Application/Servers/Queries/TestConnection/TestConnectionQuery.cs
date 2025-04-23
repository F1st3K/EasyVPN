using EasyVPN.Domain.Common.Enums;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Servers.Queries.TestConnection;

public record TestConnectionQuery(
    string Auth,
    string Endpoint,
    VpnVersion Version
    ) : IRequest<ErrorOr<Success>>;