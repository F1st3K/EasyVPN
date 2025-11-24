using EasyZsV.Domain.Common.Enums;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Servers.Queries.TestConnection;

public record TestConnectionQuery(
    string Auth,
    string Endpoint,
    ZsvVersion Version
    ) : IRequest<ErrorOr<Success>>;